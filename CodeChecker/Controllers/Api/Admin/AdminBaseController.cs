using System.Threading.Tasks;
using CodeChecker.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Admin
{
    [Authorize("CanUseAdminPanel")]
    [Route("/api/admin/[controller]/[action]/{id?}")]
    public class AdminBaseController : Controller
    {
    }
}