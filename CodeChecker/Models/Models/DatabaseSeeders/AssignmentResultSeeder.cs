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

            var assignment1 = _context.Assignments.FirstOrDefault(a => a.Name == "Theatre Square");
            var assignment2 = _context.Assignments.FirstOrDefault(a => a.Name == "Watermelon");

            var assignmentTest1 = GenerateTest("6 6 4", "4");
            assignmentTest1.Assignment = assignment1;
            _context.Add(assignmentTest1);
            _context.Add(assignmentTest1.Output);

            var assignmentTest2 = GenerateTest("8 8 4", "4");
            assignmentTest2.Assignment = assignment1;
            _context.Add(assignmentTest2);
            _context.Add(assignmentTest2.Output);

            var assignmentTest3 = GenerateTest("10 10 4", "6");
            assignmentTest3.Assignment = assignment1;
            _context.Add(assignmentTest3);
            _context.Add(assignmentTest3.Output);


            var assignmentTest21 = GenerateTest("8", "YES");
            assignmentTest21.Assignment = assignment2;
            _context.Add(assignmentTest21);
            _context.Add(assignmentTest21.Output);

            var assignmentTest22 = GenerateTest("9", "NO");
            assignmentTest22.Assignment = assignment2;
            _context.Add(assignmentTest22);
            _context.Add(assignmentTest22.Output);

            var assignmentTest23 = GenerateTest("10", "NO");
            assignmentTest23.Assignment = assignment2;
            _context.Add(assignmentTest23);
            _context.Add(assignmentTest23.Output);

            var assignmentTest24 = GenerateTest("12", "YES");
            assignmentTest24.Assignment = assignment2;
            _context.Add(assignmentTest24);
            _context.Add(assignmentTest24.Output);

            var assignments = _context.Assignments.Include(a => a.Inputs).ToList();

            foreach (var assignment in assignments)
            {
                if (assignment.Inputs.Count > 0)
                {
                    continue;
                }

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

        private Input GenerateTest(string inputText, string outputText)
        {
            var input = new Input()
            {
                Text = inputText
            };

            var output = new Output()
            {
                Text = outputText
            };

            input.Output = output;

            output.Input = input;


            return input;
        }
    }
}