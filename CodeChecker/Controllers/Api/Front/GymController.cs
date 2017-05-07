using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Models.SubmissionViewModels;
using CodeChecker.Services.CodeSubmit;
using CodeChecker.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class GymController : FrontBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AssignmentRepository _assignmentRepo;
        private readonly ApplicationUserRepository _userRepo;
        private readonly SubmissionRepository _submissionRepo;
        private readonly CodeTestTask _codeTestTask;

        public GymController(UserManager<ApplicationUser> userManager, AssignmentRepository assignmentRepo, ApplicationUserRepository userRepo, SubmissionRepository submissionRepo, CodeTestTask codeTestTask)
        {
            _userManager = userManager;
            _assignmentRepo = assignmentRepo;
            _userRepo = userRepo;
            _submissionRepo = submissionRepo;
            _codeTestTask = codeTestTask;
        }

        [HttpGet]
        public IActionResult All([FromQuery] DataFilterViewModel filterData)
        {
            var results = _assignmentRepo.GetGymAssignments(filterData);

            return Ok(Mapper.Map<IEnumerable<Assignment>>(results));
        }

        [HttpGet("{taskId}")]
        public IActionResult Get(long taskId)
        {
            var assignment = _assignmentRepo.GetByIdWithContest(taskId);

            if (assignment == null)
            {
                return BadRequest("Assignment not found");
            }

            var mappedAssignment = Mapper.Map<AssignmentViewModel>(assignment);

            return Ok(mappedAssignment);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Submit([FromBody] AssignmentSubmitViewModel assignmentSubmit)
        {
            var currentUser = _userRepo.GetUserWithContest(await GetCurrentUserAsync());
            var assignment = _assignmentRepo.GetByIdWithInputsOutputs(assignmentSubmit.AssignmentId);

            if (assignment == null)
            {
                return BadRequest("Assignment not found");
            }

            _codeTestTask.Run(new CodeAssignmentViewModel
            {
                AssignmentSubmit = assignmentSubmit,
                Assignment = assignment,
                Contest = assignment.Contest,
                Submiter = currentUser
            });

            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}