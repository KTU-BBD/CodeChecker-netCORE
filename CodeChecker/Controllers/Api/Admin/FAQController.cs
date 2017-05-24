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

namespace CodeChecker.Controllers.Api.Admin
{
    public class FAQController : AdminBaseController
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
            if ((User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            {
                var faqs = _faqRepository.GetPagedDataIncludeDeleted(filterData);
                if (faqs != null)
                    return Ok(faqs);
            }
            else
            {
                return BadRequest("Unauthorized");
            }
            return BadRequest();
        }

        

        [HttpGet("{id}")]
        public IActionResult GetFull(long id)
        {
            try
            {
                var article = _faqRepository.GetFaqFull(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    return Ok(article);
                }
                else
                {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateFaq([FromBody] EditFaqViewModel updatedFaq)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var faq = _faqRepository.GetFaqFull(updatedFaq.Id);
                    if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                    {
                        var updated = Mapper.Map(updatedFaq, faq);
                        updated.UpdatedAt = DateTime.Now;
                        _faqRepository.Update(updated);
                        return Ok("Article updated");
                    }
                }
                else
                {
                    return BadRequest("Bad FAQ data");
                }
                return BadRequest("Unauthorized");
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost("{id}")]
        public IActionResult DeleteFaq(int id)
        {
            try
            {
                var article = _faqRepository.GetFaqFull(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _faqRepository.Delete(article);
                    return Ok("Contest deleted");
                }
                else
                {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost("")]
        public IActionResult CreateFAQ([FromBody]CreateFaqViewModel question)
        {
            try
            {
                if ((User.IsInRole("Administrator") || User.IsInRole("Moderator")))
                {
                    if (question != null)
                    {
                        var neqFAQ = new Faq();

                        var assignedUser = _userRepo.GetById(_userManager.GetUserId(User));
                        neqFAQ.Creator = assignedUser;
                        neqFAQ.Question = question.Question;
                        _faqRepository.Insert(neqFAQ);
                        return Ok(neqFAQ.Id);
                    }
                }
                else {
                    return BadRequest("Unauthorized");
                }
                return BadRequest("The question of your faq cannot be empty");
            }
            catch (Exception ex)
            {

                return BadRequest("Error");
            }
        }

    }
}