using CodeChecker.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class TagSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public TagSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Tags.Any())
            {
                return;
            }
            //TODO all tags copied from codeforces, analyse unknown
            string[] tags =
            {
                "implementation",
                "dp",
                "math",
                "greedy",
                "brute force",
                "data structures",
                "constructive algorithms",
                "dfs and similar",
                "sortings",
                "binary search",
                "graphs",
                "trees",
                "strings",
                "number theory",
                "geometry",
                "combinatorics",
                "two pointers",
                "dsu",
                "bitmasks",
                "probabilities",
                "shortest paths",
                "hashing",
                "divide and conquer",
                "games",
                "matrices",
                "flows",
                "string suffix structures",
                "expression parsing",
                "graph matchings",
                "ternary search",
                "meet-in-the-middle",
                "fft",
                "2-sat",
                "chinese remainder theorem",
                "schedules",
            };

            foreach (var tag in tags)
            {
                _context.Tags.Add(new Tag()
                {
                    Name = tag
                });
            }

            _context.SaveChanges();
        }
    }
}