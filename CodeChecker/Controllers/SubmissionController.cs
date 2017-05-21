using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers
{
    public class SubmissionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}