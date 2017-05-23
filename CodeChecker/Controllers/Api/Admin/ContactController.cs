using System;
using AutoMapper;
using CodeChecker.Models.ContactViewModel;
using CodeChecker.Models.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Tasks;

namespace CodeChecker.Controllers.Api.Admin
{
    public class ContactController : AdminBaseController
    {
        private readonly ContactRepository _contactRepo;
        private readonly SendEmailTask _sendEmailTask;

        public ContactController(ContactRepository contactRepo, SendEmailTask sendEmailTask)
        {
            _contactRepo = contactRepo;
            _sendEmailTask = sendEmailTask;
        }

        [HttpGet]
        public IActionResult All([FromQuery] DataFilterViewModel filterData)
        {
            return Ok(_contactRepo.GetPagedData(filterData));
        }

        [HttpGet("{contactId}")]
        public IActionResult Get(int contactId)
        {
            var contact = _contactRepo.Get(contactId);

            if (contact == null)
            {
                return BadRequest("Contact does not exist");
            }

            return Ok(contact);
        }


        [HttpPost]
        public IActionResult Update([FromBody] ContactUpdateViewModel updatedContact)
        {
            {
                var contact = _contactRepo.Get(updatedContact.Id);
                if (contact == null)
                {
                    return BadRequest("Contact does not exist");
                }

                var updated = Mapper.Map(updatedContact, contact);
                updated.Status = ContactStatus.Answered;
                updated.UpdatedAt = DateTime.Now;
                _contactRepo.Update(updated);

                _sendEmailTask.Run(updated.Email, updated.Name, "Answer to your question", "ContactAnswer", updated);

                return Ok("Contact answered");

            }

        }
    }
}