using System;
using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class BaseSeeder
    {
        private readonly UserSeeder _userSeeder;
        private readonly RoleSeeder _roleSeeder;
        private readonly AssetSeeder _assetSeeder;
        private readonly ContestSeeder _contestSeeder;
        private readonly AssignmentSeeder _assignmentSeeder;
        private readonly AssignmentResultSeeder _assignmentResultSeeder;
        private readonly TagSeeder _tagSeeder;
        private readonly ArticleSeeder _articleSeeder;
        private readonly ApplicationDbContext _dbContext;

        public BaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userSeeder = new UserSeeder(userManager, roleManager, context);
            _roleSeeder = new RoleSeeder(roleManager);
            _assetSeeder = new AssetSeeder(context);
            _contestSeeder = new ContestSeeder(context);
            _assignmentSeeder = new AssignmentSeeder(context);
            _assignmentResultSeeder = new AssignmentResultSeeder(context);
            _tagSeeder = new TagSeeder(context);
            _articleSeeder = new ArticleSeeder(context);
            _dbContext = context;
        }

        // Do not change the order of called methods
        public async Task EnsureSeedData()
        {
            await _assetSeeder.SeedDatabase();
            await _roleSeeder.SeedDatabase();
            await _tagSeeder.SeedDatabase();
            await _userSeeder.SeedDatabase();
            await _contestSeeder.SeedDatabase();
            await _assignmentSeeder.SeedDatabase();
            await _assignmentResultSeeder.SeedDatabase();
            await _articleSeeder.SeedDatabase();
        }

        public void SendStatistic()
        {
            var date = DateTime.Now;

            _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var applicationUser in _dbContext.Users)
                {
                    _dbContext.Add(new UserStatistic()
                    {
                        CreatedAt = date,
                        Rating = applicationUser.Rating,
                        User = applicationUser
                    });
                }

                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch
            {
                _dbContext.Database.RollbackTransaction();
            }

        }
    }
}
