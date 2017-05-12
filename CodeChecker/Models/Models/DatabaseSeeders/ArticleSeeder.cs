using CodeChecker.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class ArticleSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public ArticleSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Articles.Any())
            {
                return;
            }

            var roleContributor = _context.Roles.FirstOrDefault(r => r.Name == "Contributor");
            var roleAdmin = _context.Roles.FirstOrDefault(r => r.Name == "Administrator");
            var roleMod = _context.Roles.FirstOrDefault(r => r.Name == "Moderator");

            var users = _context.Users.Include(u => u.Roles)
                .Where(ur => ur.Roles.Any(urr => urr.RoleId == roleContributor.Id || urr.RoleId == roleAdmin.Id ||
                                                 urr.RoleId == roleMod.Id))
                .ToList();


            for (int i = 0; i < new Random().Next(10, 30); i++)
            {
                var article = new Article()
                {
                    Creator = users[new Random().Next(users.Count)],
                    Title = Faker.Company.Name(),
                    ShortDescription = DescriptionFormatter(true),
                    LongDescription = DescriptionFormatter(false)
                };

                _context.Add(article);
            }

            _context.SaveChanges();
        }

        private string DescriptionFormatter(bool isShort)
        {
            var description = $"<h2>{Faker.Company.Name()}</h2><div>" +
                              $"<p>{Faker.Lorem.Paragraph()}</p>" +
                              $"<p>{Faker.Lorem.Paragraph()}</p>";
            if (isShort)
                return description;

            return description +
                   $"<p>{Faker.Lorem.Paragraph()}</p>" +
                   $"<p>{Faker.Lorem.Paragraph()}</p>";
        }
    }
}