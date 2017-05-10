using System;
using System.Threading.Tasks;
using CodeChecker.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CodeChecker.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationMiddleware(RequestDelegate next, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Invoke(HttpContext context)
        {
            var integrationTestsUserHeader = context.Request.Headers["IntegrationTestLogin"];
            if (integrationTestsUserHeader.Count > 0)
            {
                Console.WriteLine("Yes");
                var userName = integrationTestsUserHeader[0];
                var userManager = _userManager;
                var user = await userManager.FindByEmailAsync(userName);
                if (user == null)
                {
                    return;
                }
                var signInManager = _signInManager;
                var userIdentity = await signInManager.CreateUserPrincipalAsync(user);
                context.User = userIdentity;
            }

            await _next.Invoke(context);
        }

    }
}