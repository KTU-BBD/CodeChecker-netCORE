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
        private readonly InputRepository _inputRepository;
        private readonly ApplicationUserRepository _userRepo;


        public InputController(InputRepository inputRepository, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _inputRepository = inputRepository;
            _userRepo = userRepo;
        }

        [HttpPost()]
        public IActionResult UpdateTest([FromBody]InputViewModel model)
        {
            try
            {
                var input = _inputRepository.GetByIdWithOutput(model.Id);
                var updated = Mapper.Map(model, input);

                _inputRepository.Update(updated);
                return Ok(Mapper.Map<InputViewModel>(updated));
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}