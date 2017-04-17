using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class AssetSeeder
    {
        public ApplicationDbContext _context { get; set; }

        public AssetSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDatabase()
        {
            if (_context.Assets.Any())
            {
                return; // DB has been seeded
            }

            Asset defaultAvatarImage = new Asset
            {
                Name = "default.png",
                OriginalName = "default.png",
                Mimetype = "image/png"
            };

            await _context.Assets.AddAsync(defaultAvatarImage);

            await _context.SaveChangesAsync();
        }
    }
}