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

            for (int i = 0; i < 30; i++)
            {
                var startAt = DateTime.Now.AddDays(new Random().Next(3, 20));
                var contest = new Contest()
                {
                    Name = Faker.Company.Name(),
                    Password = i % 2 == 1 ? "password" : null,
                    StartAt = startAt,
                    EndAt = startAt.AddHours(new Random().Next(2, 50)),
                    Status = ContestStatus.Approved
                };

                _context.Contests.Add(contest);
                _context.SaveChanges();

                var users = _context.Users.AsQueryable().OrderBy(r => Guid.NewGuid()).Take(new Random().Next(1, 5));

                foreach (var user in users)
                {
                    _context.ContestCreators.Add(new ContestCreator
                    {
                        User = user,
                        Contest = contest
                    });
                }
                _context.SaveChanges();

            }
        }
    }
}