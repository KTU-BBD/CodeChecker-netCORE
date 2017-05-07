using System.Linq;
using AutoMapper;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using CodeChecker.Models.AssignmentViewModels.InputOutputViewModels;

namespace CodeChecker.Controllers.Api.Admin
{
    public class InputController : AdminBaseController
    {
        private InputRepository _inputRepository;
        private readonly ApplicationUserRepository _userRepo;
        private OutputRepository _outputRepository;
        UserManager<ApplicationUser> _userManager;


        public InputController(InputRepository inputRepository, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo, OutputRepository outputRepository)
        {
            _inputRepository = inputRepository;
            _userRepo = userRepo;
            _userManager = userManager;
            _outputRepository = outputRepository;
        }

        [HttpPost()]
        public IActionResult UpdateTest([FromBody]InputViewModel model)
        {
            try
            {

                var input = _inputRepository.GetByIdWithOutput(model.Id);
                if (User.IsInRole("Contributor") && input.Assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    var updated = Mapper.Map(model, input);

                    _inputRepository.Update(updated);
                    return Ok("Updated");
                }
                else {
                    return BadRequest("Unauthorized");
                }
            }

            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost()]
        public IActionResult DeleteTest([FromBody]InputViewModel model)
        {
            try
            {

                var input = _inputRepository.GetByIdWithOutput(model.Id);
                if (User.IsInRole("Contributor") && input.Assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _outputRepository.Delete(input.Output);
                    _inputRepository.Delete(input);
                    return Ok("Deleted");
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
    }
}
