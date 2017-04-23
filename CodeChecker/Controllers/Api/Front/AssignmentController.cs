using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class AssignmentController : FrontBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ContestParticipantRepository _contestParticipantRepo;
        private readonly AssignmentRepository _assignmentRepo;
        private readonly ApplicationUserRepository _userRepo;
        private readonly SubmissionRepository _submissionRepo;
        private readonly CodeSubmitService _codeSubmitService;

        public AssignmentController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, ContestParticipantRepository contestParticipantRepo,
            ApplicationUserRepository userRepo, AssignmentRepository assignmentRepo, CodeSubmitService codeSubmitService, SubmissionRepository submissionRepo)
        {
            _contestRepo = contestRepo;
            _userManager = userManager;
            _contestParticipantRepo = contestParticipantRepo;
            _userRepo = userRepo;
            _assignmentRepo = assignmentRepo;
            _codeSubmitService = codeSubmitService;
            _submissionRepo = submissionRepo;
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(long taskId)
        {
            var currentUser = _userRepo.GetUserWithContest(await GetCurrentUserAsync());

            if (currentUser == null)
            {
                return BadRequest("You need to login to submit task");
            }

            var assignment = _assignmentRepo.GetByIdWithContest(taskId);

            foreach (var participant in assignment.Contest.ContestParticipants)
            {
                if (participant.UserId == currentUser.Id)
                {
                    return Ok(Mapper.Map<AssignmentViewModel>(assignment));
                }
            }

            return BadRequest("Cannot view this task");
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] AssignmentSubmitViewModel assignmentSubmit)
        {
            var currentUser = _userRepo.GetUserWithContest(await GetCurrentUserAsync());

            if (currentUser == null)
            {
                return BadRequest("You need to login to submit task");
            }

            var assignment = _assignmentRepo.GetByIdWithInputsOutputs(assignmentSubmit.AssignmentId);

            if (assignment == null)
            {
                return BadRequest("Assignment not found");
            }

            foreach (var input in assignment.Inputs)
            {
                var code = new CodeSubmitViewModel
                {
                    code = assignmentSubmit.Code,
                    language = assignmentSubmit.Language,
                    memoryLimit = assignment.MemoryLimit,
                    timeLimit = assignment.TimeLimit,
                    inputText = input.Text
                };

                var results = await _codeSubmitService.SubmitCode(code);

                bool verdict = results.Output == input.Output.Text;

                _submissionRepo.Insert(new Submission
                {
                    Assignment = assignment,
                    Code = assignmentSubmit.Code,
                    Language = assignmentSubmit.Language,
                    TimeMs = (int) (results.TimeSpent * 1000),
                    User = currentUser,
                    Verdict = verdict ? "YES" : "NO"
                });

                if (!verdict)
                {
                    return BadRequest("Failed at tests");
                }
            }

            return Ok();

        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}