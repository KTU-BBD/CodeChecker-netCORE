using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using CodeChecker.Models.Models;
using CodeChecker.Models.ServiceViewModels;

namespace CodeChecker.Controllers.Api.Admin
{
    public class FAQController : AdminBaseController
    {

        private FAQRepository _faqRepository;
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserRepository _userRepo;







        //[HttpGet]
        //public IActionResult GetAll([FromQuery] DataFilterViewModel filterData)
        //{
        //    if ((User.IsInRole("Administrator") || User.IsInRole("Moderator")))
        //    {
        //        var articles = _articleRepo.GetPagedDataIncludeDeleted(filterData);
        //        if (articles != null)
        //            return Ok(articles);
        //    }
        //    else
        //    {
        //        var userId = _userManager.GetUserId(User);
        //        var query = _articleRepo.QueryDeletedWithCreator().Where(c => c.Creator.Id == userId);

        //        var articles = _articleRepo.GetPagedData(query, filterData);

        //        if (articles != null)
        //        {
        //            return Ok(articles);
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}