using System.Threading.Tasks;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Services.CodeSubmit;
using CodeChecker.Services.EmailSending;
using CodeChecker.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers
{
    public class HomeController : BaseController
    {
        private SendEmailTask _sendEmailTask;

        public HomeController(SendEmailTask sendEmailTask)
        {
            _sendEmailTask = sendEmailTask;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}