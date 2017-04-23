using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers
{
    public class HomeController : BaseController
    {
        private CodeSubmitService _service;

        public HomeController(CodeSubmitService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
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
