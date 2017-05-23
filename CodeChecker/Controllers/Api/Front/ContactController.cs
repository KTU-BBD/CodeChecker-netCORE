using System.Collections.Generic;
using AutoMapper;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.ContactViewModel;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class ContactController : FrontBaseController
    {
        private readonly ContactRepository _contactRepo;

        public ContactController(ContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        [HttpPost]
        public IActionResult Submit([FromBody] ContactSubmitViewModel submitViewModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        return BadRequest(error.ErrorMessage);
                    }
                }
            }

            _contactRepo.Insert(new Contact()
            {
                Name = submitViewModel.Name,
                Email = submitViewModel.Email,
                Message = submitViewModel.Message
            });

            return Ok("Your message was sent succesfully");
        }
    }
}