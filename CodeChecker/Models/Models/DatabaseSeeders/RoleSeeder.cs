using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class RoleSeeder
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedDatabase()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";

                await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";

                await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Moderator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Moderator";

                await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Contributor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Contributor";

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
