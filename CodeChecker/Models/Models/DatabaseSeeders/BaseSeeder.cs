﻿using CodeChecker.Data;
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

        public BaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userSeeder = new UserSeeder(userManager, roleManager, context);
            _roleSeeder = new RoleSeeder(roleManager);
            _assetSeeder = new AssetSeeder(context);
        }

        // Do not change the order of called methods
        public async Task EnsureSeedData()
        {
            await _assetSeeder.SeedDatabase();
            await _roleSeeder.SeedDatabase();
            await _userSeeder.SeedDatabase();
        }
    }
}
