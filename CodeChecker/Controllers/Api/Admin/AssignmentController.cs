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
        private ContestRepository _contestRepo;


        public AssignmentController(AssignmentRepository assignmentRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo, ContestRepository contestRepo)
        {
            _assignmentRepo = assignmentRepo;
            _userRepo = userRepo;
            _userManager = userManager;
            _contestRepo = contestRepo;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAssignmentViewmodel model)
        {
            try {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception("Bad assignment name or contest ID");
                }
                else {
                    var assignment = new Assignment();
                    assignment.Name = model.Name;
                    var con = _contestRepo.GetContestFull(model.ContestID);
                    assignment.Contest = con;
                    _assignmentRepo.Insert(assignment);
                    var assignmentToReturn = Mapper.Map<ShortAssignmentViewModel>(assignment);
                    return Ok(assignmentToReturn);
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
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
                    assignmentToReturn.Inputs = assignmentToReturn.Inputs.OrderByDescending(x => x.Id).ToList();
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
                    updated.UpdatedAt = DateTime.Now;
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

        [HttpGet("{id}")]
        public IActionResult CreateTest(long id)
        {
            try
            {
                var assignment = _assignmentRepo.GetByIdWithInputsOutputs(id);
                if (User.IsInRole("Contributor") && assignment.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _assignmentRepo.CreateTestForAssignment(id);
                    return Ok("Test created");
                }
                else {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception");

            }
        }
    }
}