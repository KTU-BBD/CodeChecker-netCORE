using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class ContestSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public ContestSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (DynamicQueryableExtensions.Any(_context.Contests))
            {
                return; // DB has been seeded
            }

            var contestStatuses = Enum.GetValues(typeof(ContestStatus));
            var contestTypes = Enum.GetValues(typeof(ContestType));

            var roleContributor = _context.Roles.FirstOrDefault(r => r.Name == "Contributor");
            var roleAdmin = _context.Roles.FirstOrDefault(r => r.Name == "Administrator");
            var roleMod = _context.Roles.FirstOrDefault(r => r.Name == "Moderator");

            var users = _context.Users.Include(u => u.Roles)
                .Where(ur => ur.Roles.Any(urr => urr.RoleId == roleContributor.Id || urr.RoleId == roleAdmin.Id ||
                                                 urr.RoleId == roleMod.Id))
                .ToList();

            var contest1 = CreateCustomContest("Fully working contest #1",
                "<h2>This is fully working contest</h2> <p>This text is random, but tasks are actualy working</p>",
                users.First());

            _context.Contests.Add(contest1);

            Debug.WriteLine(contest1.Description);


            _context.Contests.Add(CreateCustomContest("Fully working contest #2",
                "<h2>This is fully working contest</h2> <p>This text is random, but tasks are actualy working</p>",
                users.First()));

            _context.SaveChanges();
//
            for (int i = 0; i < 50; i++)
            {
                var startAt = DateTime.Now.AddDays(new Random().Next(-10, 10));
                var contest = new Contest()
                {
                    Name = Faker.Company.Name() + " - " + i,
                    Password = i % 2 == 1 ? "password" : null,
                    StartAt = startAt,
                    EndAt = startAt.AddHours(new Random().Next(2, 50)),
                    Status = (ContestStatus) contestStatuses.GetValue(new Random().Next(contestStatuses.Length)),
                    Type = (ContestType) contestTypes.GetValue(new Random().Next(contestTypes.Length)),
                    Description = DescriptionFormatter(),
                    Creator = users[new Random().Next(users.Count)]
                };

                _context.Contests.Add(contest);
                _context.SaveChanges();
            }
        }


        private Contest CreateCustomContest(string name, string description, ApplicationUser user)
        {
            return new Contest()
            {
                Name = name,
                Password = null,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddHours(300),
                Status = ContestStatus.Approved,
                Type = ContestType.Contest,
                Description = description,
                Creator = user
            };
        }

        private string DescriptionFormatter()
        {
            return
                $"<h2>{Faker.Company.Name()}</h2><div>Hello and welcome to our contest!<br>This task is soo awesome!<br>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>";
        }
    }
}