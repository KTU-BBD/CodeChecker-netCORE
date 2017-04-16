using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class UtilController : FrontBaseController
    {
        public IActionResult Test()
        {
            return Ok("Hello World");
        }
    }
}