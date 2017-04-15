using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class BaseSeeder
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        protected ApplicationDbContext _context { get; set; }
        private UserSeeder _userSeeder;
        private RoleSeeder _roleSeeder;
        public BaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _userSeeder = new UserSeeder(userManager, roleManager, context);
            _roleSeeder = new RoleSeeder(_roleManager);
        }
        // Do not change the order of called methods
        public async Task EnsureSeedData()
        {
            await _roleSeeder.SeedDatabase();
            await _userSeeder.SeedDatabase();
        }
    }
}
