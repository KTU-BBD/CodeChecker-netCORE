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
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.AssignmentViewModels.InputOutputViewModels;

namespace CodeChecker.Controllers.Api.Admin
{
    public class AssignmentController : AdminBaseController
    {
        private readonly AssignmentRepository _assignmentRepo;
        private readonly ApplicationUserRepository _userRepo;


        public AssignmentController(AssignmentRepository assignmentRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _assignmentRepo = assignmentRepo;
            _userRepo = userRepo;
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] EditAssignmentPostViewModel contest)
        {
           // TODO
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public IActionResult GetFull(long id)
        {
            try
            {
                var assignment = _assignmentRepo.GetByIdWithInputsOutputs(id);
                var assignmentToReturn = Mapper.Map<EditAssignmentGetViewModel>(assignment);
                return Ok(assignmentToReturn);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost]
        public IActionResult Update([FromBody]EditAssignmentPostViewModel updatedAssignment)
        {

            try
            {
                // FIX THIS PART
                var assignment = _assignmentRepo.GetById(updatedAssignment.Id);
                var updated = Mapper.Map(updatedAssignment, assignment);

                _assignmentRepo.Update(updated);
                return Ok(Mapper.Map<EditAssignmentPostViewModel>(updated));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}