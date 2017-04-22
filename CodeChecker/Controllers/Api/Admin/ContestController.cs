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

namespace CodeChecker.Controllers.Api.Admin
{
    public class ContestController : AdminBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly ApplicationUserRepository _userRepo;
        private readonly ContestCreatorRepository _contestCreatorRepo;

        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo, ContestCreatorRepository contestCreatorRepo)
        {
            _contestRepo = contestRepo;
            _userRepo = userRepo;
            _contestCreatorRepo = contestCreatorRepo;
        }

        [HttpGet("")]
        public IActionResult All([FromQuery] DataFilterViewModel filterData)
        {
            return Ok(_contestRepo.GetPagedData(filterData));
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreateContestViewModel contest)
        {
            if (ModelState.IsValid)
            {
                var newContest = Mapper.Map<Contest>(contest);
                _contestRepo.Insert(newContest);

                var assignedUsers = _userRepo.GetByIds(contest.Creators);

                if (assignedUsers.Count() == 0)
                {
                    ModelState.AddModelError("Creators", "Need atleast one creator");
                    return BadRequest(ModelState);
                }

                foreach (var user in assignedUsers)
                {
                    _contestCreatorRepo.Insert(new ContestCreator {ContestId = newContest.Id, UserId = user.Id}, false);
                }

                _contestCreatorRepo.Save();

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("{id}")]
        public IActionResult GetByID(long id)
        {
            try
            {
                var contest = _contestRepo.Get(id);
                return Ok(contest);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}