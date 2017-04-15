using CodeChecker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task EnsureSeedData()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";

                await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("SimpleUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "SimpleUser";

                await _roleManager.CreateAsync(role);
            }
            for (int i = 0; i < 10000; i++)
            {
                var user = new ApplicationUser()
                {
                    UserName = "Euzabiejus" + i.ToString("00000"),
                    Email = $"erlandas{i.ToString("00000")}@gmail.com"
                };
               await  _userManager.CreateAsync(user, "P@ssw0rd!");
                if(i < 10)
                _userManager.AddToRoleAsync(user,"Administrator").Wait();
                else
                    _userManager.AddToRoleAsync(user,"SimpleUser").Wait();
            }
            await _context.SaveChangesAsync();
        }
    }
}
