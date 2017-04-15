using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers
{
    [Authorize("CanUseAdminPanel")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}