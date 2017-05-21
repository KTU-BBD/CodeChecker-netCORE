using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CodeChecker.Models.EmailMessageModels;

namespace CodeChecker.Controllers.Api.Front
{
    public class ContestController : FrontBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ContestParticipantRepository _contestParticipantRepo;
        private readonly ApplicationUserRepository _userRepo;
        private readonly SendEmailTask _sendEmailTask;

        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, ContestParticipantRepository contestParticipantRepo,
            ApplicationUserRepository userRepo, SendEmailTask sendEmailTask)
        {
            _contestRepo = contestRepo;
            _userManager = userManager;
            _contestParticipantRepo = contestParticipantRepo;
            _userRepo = userRepo;
            _sendEmailTask = sendEmailTask;
        }

        [HttpGet("")]
        public async Task<IActionResult> All([FromQuery] DataFilterViewModel filterData)
        {
            var model = new CreateDeleteMessageViewModel
            {
                Reason = "Test reason is test",
                CreatorName = "Jonas Ketvirtis",
                ItemName = "Test article",
                ItemId = 3
            };
            _sendEmailTask.Run("test3@email.test", model.CreatorName, "subject", "ArticleRejection", model);

            var contests = _contestRepo.GetActiveContestPagedData(filterData);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                foreach (var contest in contests)
                {
                    foreach (var participant in contest.ContestParticipants)
                    {
                        if (participant.UserId == user.Id)
                        {
                            contest.Joined = true;
                        }
                    }
                }
            }

            return Ok(Mapper.Map<IEnumerable<ContestViewModel>>(contests));
        }

        [HttpGet("{contestId}")]
        public async Task<IActionResult> Get(long contestId)
        {
            var currentUser = _userRepo.GetUserWithContest(await GetCurrentUserAsync());
            var contest = _contestRepo.GetContestWithAssignments(contestId);

            if (contest == null)
            {
                return BadRequest("Contest not found");
            }

            if (contest.StartAt > DateTime.Now)
            {
                return BadRequest("Contest is not started yet");
            }

            if (currentUser != null)
            {
                foreach (var participant in currentUser.ContestParticipants)
                {
                    if (participant.ContestId == contest.Id)
                    {
                        return Ok(Mapper.Map<ContestWithAssignmentViewModel>(contest));
                    }
                }
            }
            else
            {
                return BadRequest("You need to log in to view contest");
            }

            return BadRequest("You cannot view this contest");
        }

        [HttpPost]
        public async Task<IActionResult> Join([FromBody] ContestJoinViewModel contestData)
        {
            var contest = _contestRepo.Get(contestData.ContestId);
            var user = await GetCurrentUserAsync();

            if (contest == null)
            {
                return NotFound("Contest not found");
            }

            if (user == null)
            {
                return BadRequest("You need to login to join contest");
            }

            //Investigate how to avoid errors in console, when trying to join
            try
            {
                if (string.IsNullOrEmpty(contest.Password) || contest.Password.Equals(contestData.Password))
                {
                    _contestParticipantRepo.Insert(new ContestParticipant
                    {
                        Contest = contest,
                        User = user
                    });
                }
                else
                {
                    return BadRequest("Bad contest password provided");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot join same contest twice");
            }

            return Ok("Joined contest successfully");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}