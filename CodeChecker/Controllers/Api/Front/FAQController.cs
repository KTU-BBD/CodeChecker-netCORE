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
using CodeChecker.Models.FAQViewModel;
using AutoMapper;
using CodeChecker.Controllers.Api.Admin;

namespace CodeChecker.Controllers.Api.Front
{
    public class FAQController : FrontBaseController
    {

        private FAQRepository _faqRepository;
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserRepository _userRepo;



        public FAQController(FAQRepository faqRepository, UserManager<ApplicationUser> userManager,
            ApplicationUserRepository userRepo) {
            _faqRepository = faqRepository;
            _userManager = userManager;
            _userRepo = userRepo;
        }



        [HttpGet]
        public IActionResult GetAll([FromQuery] DataFilterViewModel filterData)
        {
                var faqs = _faqRepository.GetPagedDataIncludeDeleted(filterData);
                if (faqs != null)
                    return Ok(faqs);
                return BadRequest();
        }

        

        [HttpGet("{id}")]
        public IActionResult GetFull(long id)
        {
            try
            {
                var article = _faqRepository.GetFaqFull(id);
                return Ok(article);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}