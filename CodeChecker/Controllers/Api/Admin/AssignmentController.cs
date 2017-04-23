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
                var assignment = _assignmentRepo.Get(id);
                var assignmentToReturn = Mapper.Map<EditAssignmentGetViewModel>(assignment);
                return Ok(assignmentToReturn);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost]
        //public IActionResult Update([FromBody]EditContestPostViewModel updatedContest)
        //{

        //    try
        //    {

        //        var contest = _assignmentRepo.GetContestFull(updatedContest.Id);
        //        var updated = Mapper.Map(updatedContest, contest);

        //        _assignmentRepo.Update(updated);
        //        return Ok(Mapper.Map<EditContestPostViewModel>(updated));
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        return BadRequest(ex);
        //    }
        //}
    }
}