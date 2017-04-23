using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers
{
    public class ContestController : BaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserRepository _userRepo;

        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _contestRepo = contestRepo;
            _userManager = userManager;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}