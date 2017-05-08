using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models.Enums;

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

            for (int i = 0; i < 50; i++)
            {
                var startAt = DateTime.Now.AddDays(new Random().Next(-10, 10));
                var user = _context.Users.AsQueryable().OrderBy(r => Guid.NewGuid()).First();
                var contest = new Contest()
                {
                    Name = Faker.Company.Name() + " - " + i,
                    Password = i % 2 == 1 ? "password" : null,
                    StartAt = startAt,
                    EndAt = startAt.AddHours(new Random().Next(2, 50)),
                    Status = (ContestStatus)contestStatuses.GetValue(new Random().Next(contestStatuses.Length)),
                    Type = (ContestType)contestTypes.GetValue(new Random().Next(contestTypes.Length)),
                    Description = DescriptionFormatter(),
                    Creator = user
                };

                _context.Contests.Add(contest);
                _context.SaveChanges();
            }
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