using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    [Route("/api/front/[controller]/[action]/{id?}")]
    public class FrontBaseController : Controller
    {
    }
}