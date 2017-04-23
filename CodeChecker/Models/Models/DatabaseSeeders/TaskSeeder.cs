using CodeChecker.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class TaskSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public TaskSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Assignments.Any())
            {
                return;
            }

            var contests = _context.Contests.Include(c => c.ContestCreators).ThenInclude(u => u.User).ToList();
            foreach (var contest in contests)
            {

                for (int i = 0; i < new Random().Next(3, 6); i++)
                {
                    var assignment = new Assignment()
                    {
                        Contest = contest,
                        MemoryLimit = new Random().Next(100, 400),
                        SolvedCount = 0,
                        TimeLimit = new Random().Next(1000, 8000),
                        Creator = contest.ContestCreators.First().User,
                        MaxPoints = new Random().Next(300, 1500),
                        Name = Faker.Company.Name(),
                        Description = DescriptionFormatter(),
                        IsActive = true,
                        InputType = "standard input",
                        OutputType = "standard output"
                    };

                    _context.Assignments.Add(assignment);
                    _context.SaveChanges();
                }
            }
        }

        private string DescriptionFormatter()
        {
            return
                $"<h2>{Faker.Company.Name()}</h2><div>Hello and welcome to our test task!<br>This task is soo awesome!<br>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>" +
                $"<p>{Faker.Lorem.Paragraph()}</p>";
        }
    }
}