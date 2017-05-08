using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Models.SubmissionViewModels;
using CodeChecker.Services.CodeSubmit;
using CodeChecker.Tasks;
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
        private CodeSubmitService _codeSubmitService;
        private CodeTestTask _codeTestTask;
        private ApplicationDbContext _context;

        public AssignmentController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, ContestParticipantRepository contestParticipantRepo,
            ApplicationUserRepository userRepo, AssignmentRepository assignmentRepo,
            CodeSubmitService codeSubmitService, SubmissionRepository submissionRepo, CodeTestTask codeTestTask)
        {
            _contestRepo = contestRepo;
            _userManager = userManager;
            _contestParticipantRepo = contestParticipantRepo;
            _userRepo = userRepo;
            _assignmentRepo = assignmentRepo;
            _codeSubmitService = codeSubmitService;
            _submissionRepo = submissionRepo;
            _codeTestTask = codeTestTask;
            _context = context;
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(long taskId)
        {
            var assignment = _assignmentRepo.GetByIdWithContest(taskId);

            if (assignment == null)
            {
                return BadRequest("Assignment not found");
            }

            var currentUser = await GetCurrentUserAsync();


            if (assignment.Contest.Type == ContestType.Gym || assignment.Contest.EndAt < DateTime.Now)
            {
                var mappedAssignment = Mapper.Map<AssignmentViewModel>(assignment);
                if (currentUser != null)
                {
                    var lastSubmission = _submissionRepo.GetLastUserSubmissionInContest(currentUser, assignment);
                    mappedAssignment.LastSubmission = Mapper.Map<LastSubmissionViewModel>(lastSubmission);
                }

                return Ok(mappedAssignment);
            }

            var userWithContests = _userRepo.GetUserWithContest(currentUser);

            if (userWithContests == null)
            {
                return BadRequest("You need to login to submit task");
            }


            foreach (var participant in assignment.Contest.ContestParticipants)
            {
                if (participant.UserId == userWithContests.Id)
                {
                    var mappedAssignment = Mapper.Map<AssignmentViewModel>(assignment);
                    var lastSubmission = _submissionRepo.GetLastUserSubmissionInContest(userWithContests, assignment);
                    mappedAssignment.LastSubmission = Mapper.Map<LastSubmissionViewModel>(lastSubmission);

                    return Ok(mappedAssignment);
                }
            }

            return BadRequest("Cannot view this task");
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] AssignmentSubmitViewModel assignmentSubmit)
        {
            //TODO Implemenet feature to calculate points
            //TODO AND DO NOT FORGET TO IGNORE GYM POINTS

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return BadRequest("You need to login to submit code");
            }

            var userWithContest = _userRepo.GetUserWithContest(currentUser);

            if (userWithContest == null)
            {
                return BadRequest("You need to login to submit task");
            }


            var assignment = _assignmentRepo.GetByIdWithInputsOutputs(assignmentSubmit.AssignmentId);
            if (assignment == null)
            {
                return BadRequest("Assignment not found");
            }

            var canSubmit = false;
            foreach (var contest in userWithContest.ContestParticipants)
            {
                if (contest.ContestId == assignment.Contest.Id)
                {
                    canSubmit = true;
                    break;
                }
            }

            if (assignment.Contest.Type != ContestType.Gym && assignment.Contest.EndAt > DateTime.Now && !canSubmit)
            {
                return BadRequest("You can't submit your code");
            }

            _codeTestTask.Run(new CodeAssignmentViewModel()
            {
                AssignmentSubmit = assignmentSubmit,
                AssignmentId = assignment.Id,
                SubmiterId = userWithContest.Id
            });

            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}