using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class GymController : FrontBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ContestParticipantRepository _contestParticipantRepo;
        private readonly ApplicationUserRepository _userRepo;

        public GymController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, ContestParticipantRepository contestParticipantRepo,
            ApplicationUserRepository userRepo)
        {
            _contestRepo = contestRepo;
            _userManager = userManager;
            _contestParticipantRepo = contestParticipantRepo;
            _userRepo = userRepo;
        }

        [HttpGet("")]
        public async Task<IActionResult> All([FromQuery] DataFilterViewModel filterData)
        {
            var contests = _contestRepo.GetActiveGymPagedData(filterData);
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
            var contest = _contestRepo.GetGymWithAssignments(contestId);

            if (contest == null)
            {
                return BadRequest("Gym not found");
            }

            if (contest.StartAt > DateTime.Now)
            {
                return BadRequest("Gym is not started yet");
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
                return BadRequest("You need to log in to view gym");
            }

            return BadRequest("You cannot view this gym");
        }

        [HttpPost]
        public async Task<IActionResult> Join([FromBody] ContestJoinViewModel contestData)
        {
            var contest = _contestRepo.Get(contestData.ContestId);
            var user = await GetCurrentUserAsync();

            if (contest == null)
            {
                return NotFound("Gym not found");
            }

            if (user == null)
            {
                return BadRequest("You need to login to join gym");
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
                    return BadRequest("Bad gym password provided");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot join same gym twice");
            }

            return Ok("Joined gym successfully");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}