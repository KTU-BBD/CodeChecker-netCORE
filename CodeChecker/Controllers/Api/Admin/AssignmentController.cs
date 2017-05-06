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
        private readonly UserManager<ApplicationUser> _userManager;


        public AssignmentController(AssignmentRepository assignmentRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _assignmentRepo = assignmentRepo;
            _userRepo = userRepo;
            _userManager = userManager;
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
                if (User.IsInRole("Contributor") && assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    var assignmentToReturn = Mapper.Map<EditAssignmentGetViewModel>(assignment);
                    return Ok(assignmentToReturn);
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



        [HttpPost]
        public IActionResult Update([FromBody]EditAssignmentPostViewModel updatedAssignment)
        {
            try
            {
                var assignment = _assignmentRepo.GetById(updatedAssignment.Id);
                if (User.IsInRole("Contributor") && assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    var updated = Mapper.Map(updatedAssignment, assignment);

                    _assignmentRepo.Update(updated);
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

        [HttpPost]
        public IActionResult Delete([FromBody]EditAssignmentPostViewModel updatedAssignment)
        {
            try
            {
                var assignment = _assignmentRepo.GetByIdWithInputsOutputs(updatedAssignment.Id);
                if (User.IsInRole("Contributor") && assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    

                    _assignmentRepo.Delete(assignment);
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