using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models.DatabaseSeeders
{
    public class UserSeeder
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContext _context { get; set; }

        public UserSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public  async Task SeedDatabase()
        {
            if (_userManager.FindByEmailAsync("Arvydas@Daubaris.lt") == null)
            {
                var userArvydas = new ApplicationUser()
                {
                    UserName = "Arvydas",
                    Email = "Arvydas@Daubaris.lt"
                };
                await _userManager.CreateAsync(userArvydas, "P@ssw0rd!");
                _userManager.AddToRoleAsync(userArvydas, "Administrator").Wait();
            }

            if (_userManager.FindByEmailAsync("Erlandas.Trumpickas@gmail.com") == null)
            {
                var userErlandas = new ApplicationUser()
                {
                    UserName = "Erlandas",
                    Email = "Erlandas.Trumpickas@gmail.com"
                };
                await _userManager.CreateAsync(userErlandas, "P@ssw0rd!");
                _userManager.AddToRoleAsync(userErlandas, "Administrator").Wait();
            }

            if (_context.Users.Any())
            {
                return;   // DB has been seeded
            }
            
            for (int i = 0; i < 100; i++)
            {
                var user = new ApplicationUser()
                {
                    UserName = Faker.Internet.UserName(),
                    Email = Faker.Internet.Email()
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (i < 5)
                    _userManager.AddToRoleAsync(user, "Administrator").Wait();
                else if (i >= 5 && i < 10)
                    _userManager.AddToRoleAsync(user, "Moderator").Wait();
                else if (i >= 10 && i < 30)
                    _userManager.AddToRoleAsync(user, "Contributor").Wait();
                else 
                    _userManager.AddToRoleAsync(user, "User").Wait();
            }
            await _context.SaveChangesAsync();
        }
    }
}
