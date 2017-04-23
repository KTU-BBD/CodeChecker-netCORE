using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class BaseSeeder
    {
        private readonly UserSeeder _userSeeder;
        private readonly RoleSeeder _roleSeeder;
        private readonly AssetSeeder _assetSeeder;
        private readonly ContestSeeder _contestSeeder;
        private readonly TaskSeeder _taskSeeder;
        private readonly TaskResultSeeder _taskResultSeeder;

        public BaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userSeeder = new UserSeeder(userManager, roleManager, context);
            _roleSeeder = new RoleSeeder(roleManager);
            _assetSeeder = new AssetSeeder(context);
            _contestSeeder = new ContestSeeder(context);
            _taskSeeder = new TaskSeeder(context);
            _taskResultSeeder = new TaskResultSeeder(context);
        }

        // Do not change the order of called methods
        public async Task EnsureSeedData()
        {
            await _assetSeeder.SeedDatabase();
            await _roleSeeder.SeedDatabase();
            await _userSeeder.SeedDatabase();
            await _contestSeeder.SeedDatabase();
            await _taskSeeder.SeedDatabase();
            await _taskResultSeeder.SeedDatabase();
        }
    }
}
