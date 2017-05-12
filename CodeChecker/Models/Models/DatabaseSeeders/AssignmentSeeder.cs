using CodeChecker.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class AssignmentSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public AssignmentSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Assignments.Any())
            {
                return;
            }

            var workingContest1 = _context.Contests.Include(c => c.Creator)
                .First(c => c.Name == "Fully working contest #1");
            var workingContest2 = _context.Contests.Include(c => c.Creator)
                .First(c => c.Name == "Fully working contest #2");

            var assignment1 = CreateCustomAssignment(workingContest1, workingContest1.Creator,
                "<p>Theatre Square in the capital city of Berland has a rectangular shape with the size <span class=\"tex-span\"><i>n</i> × <i>m</i></span> meters. On the occasion of the city's anniversary, a decision was taken to pave the Square with square granite flagstones. Each flagstone is of the size <span class=\"tex-span\"><i>a</i> × <i>a</i></span>.</p>" +
                "<p>What is the least number of flagstones needed to pave the Square? It's allowed to cover the surface larger than the Theatre Square, but the Square has to be covered. It's not allowed to break the flagstones. The sides of flagstones should be parallel to the sides of the Square.</p>" +
                "<div class=\"section-title\">Input</div>" +
                "<p>The input contains three positive integer numbers in the first line: <span class=\"tex-span\"><i>n</i>,  <i>m</i></span> and <span class=\"tex-span\"><i>a</i></span> (<span class=\"tex-span\">1 ≤  <i>n</i>, <i>m</i>, <i>a</i> ≤ 10<sup class=\"upper-index\">9</sup></span>).</p>" +
                "<div class=\"section-title\">Output</div>" +
                "<p>Write the needed number of flagstones.</p>" +
                "<div class=\"section-title\">Examples</div><div class=\"sample-test\"><div class=\"input\"><div class=\"title\">Input</div><pre>6 6 4<br></pre></div><div class=\"output\"><div class=\"title\">Output</div><pre>4<br></pre></div></div>",
                "Theatre Square");

            var assignment2 = CreateCustomAssignment(workingContest2, workingContest2.Creator,
                "<p>One hot summer day Pete and his friend Billy decided to buy a watermelon. They chose the biggest and the ripest one, in their opinion. After that the watermelon was weighed, and the scales showed <span class=\"tex-span\"><i>w</i></span> kilos. They rushed home, dying of thirst, and decided to divide the berry, however they faced a hard problem.</p><p>Pete and Billy are great fans of even numbers, that\'s why they want to divide the watermelon in such a way that each of the two parts weighs even number of kilos, at the same time it is not obligatory that the parts are equal. The boys are extremely tired and want to start their meal as soon as possible, that\'s why you should help them and find out, if they can divide the watermelon in the way they want. For sure, each of them should get a part of positive weight.</p>" +
                "<div class=\"section-title\">Input</div><p>The first (and the only) input line contains integer number <span class=\"tex-span\"><i>w</i></span> (<span class=\"tex-span\">1 ≤ <i>w</i> ≤ 100</span>) — the weight of the watermelon bought by the boys.</p>" +
                "<div class=\"section-title\">Output</div><p>Print <span class=\"tex-font-style-tt\">YES</span>, if the boys can divide the watermelon into two parts, each of them weighing even number of kilos; and <span class=\"tex-font-style-tt\">NO</span> in the opposite case.</p>" +
                "<div class=\"section-title\">Examples</div><div class=\"sample-test\"><div class=\"input\"><div class=\"title\">Input</div><pre>8<br></pre></div><div class=\"output\"><div class=\"title\">Output</div><pre>YES<br></pre></div></div>" +
                "<div class=\"section-title\">Note</div><p>For example, the boys can divide the watermelon into two parts of 2 and 6 kilos respectively (another variant — two parts of 4 and 4 kilos).</p>",
                "Watermelon");

            _context.Add(assignment1);
            _context.Add(assignment2);

            var contests = _context.Contests.Include(c => c.Creator).Include(c=>c.Assignments).ToList();
            var tags = _context.Tags.ToList();
            foreach (var contest in contests)
            {
                if (contest.Assignments.Count > 0)
                {
                    continue;
                }
                for (int i = 0; i < new Random().Next(3, 6); i++)
                {
                    var assignment = new Assignment()
                    {
                        Contest = contest,
                        MemoryLimit = new Random().Next(100, 400),
                        SolvedCount = 0,
                        TimeLimit = new Random().Next(1000, 8000),
                        Creator = contest.Creator,
                        MaxPoints = new Random().Next(300, 1500),
                        Name = Faker.Company.Name(),
                        Description = DescriptionFormatter(),
                        IsActive = true
                    };

                    _context.Assignments.Add(assignment);
                    _context.SaveChanges();

                    for (int j = 0; j < new Random().Next(1, 4); j++)
                    {
                        _context.AssignmentTags.Add(new AssignmentTag()
                        {
                            Assignment = assignment,
                            Tag = tags[new Random().Next(4, tags.Count - 4) + j]
                        });
                    }

                    _context.SaveChanges();
                }
            }
        }

        private Assignment CreateCustomAssignment(Contest contest, ApplicationUser creator, string description,
            string name)
        {
            return new Assignment()
            {
                Contest = contest,
                Creator = creator,
                MaxPoints = 500,
                TimeLimit = 5000,
                MemoryLimit = 256,
                Description = description,
                Name = name,
            };
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