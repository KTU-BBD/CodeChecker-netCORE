using CodeChecker.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class AssignmentResultSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public AssignmentResultSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Inputs.Any() || _context.Outputs.Any())
            {
                return;
            }


            var assignments = _context.Assignments.ToList();

            foreach (var assignment in assignments)
            {
                for (int i = 0; i < new Random().Next(4, 12); i++)
                {
                    var randomText = Faker.Name.First();

                    var input = new Input()
                    {
                        Assignment = assignment,
                        Text = randomText,
                    };

                    _context.Inputs.Add(input);
                    _context.SaveChanges();

                    var output = new Output
                    {
                        Text = randomText,
                        InputId = input.Id
                    };

                    _context.Outputs.Add(output);
                    _context.SaveChanges();
                }
            }
        }
    }
}