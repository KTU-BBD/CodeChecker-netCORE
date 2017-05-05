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
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Controllers.Api.Admin
{
    public class ContestController : AdminBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly ApplicationUserRepository _userRepo;
        private UserManager<ApplicationUser> _userManager;


        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager, ApplicationUserRepository userRepo)
        {
            _contestRepo = contestRepo;
            _userRepo = userRepo;
            _userManager = userManager;
        }

        [HttpGet("")]
        public IActionResult All([FromQuery] DataFilterViewModel filterData)
        {
            if ((User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            {
                var contests = _contestRepo.GetPagedData(filterData);
                if (contests != null)
                    return Ok(contests);
            }
            if (User.IsInRole("Contributor"))
            {
                var contests = _contestRepo.GetPagedDataForContributor(filterData);
                var offset = filterData.Count * (filterData.Page - 1);
                var userId = _userManager.GetUserId(User);
                contests = contests.Where(x => x.Creator.Id == userId).Skip(offset).Take(filterData.Count);
                if (contests != null)
                {
                    return Ok(contests);
                }
                else {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreateContestViewModel contest)
        {
            if (ModelState.IsValid)
            {
                var newContest = Mapper.Map<Contest>(contest);
                _contestRepo.Insert(newContest);

                var assignedUsers = _userRepo.GetById(contest.Creator);

                if (assignedUsers == null)
                {
                    ModelState.AddModelError("Creators", "Need atleast one creator");
                    return BadRequest(ModelState);
                }


                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var contest = _contestRepo.Get(id);
                return Ok(contest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFull(long id)
        {
            try
            {
                var contest = _contestRepo.GetContestFull(id);
                var contestToReturn = Mapper.Map<EditContestGetViewModel>(contest);
                return Ok(contestToReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody]EditContestPostViewModel updatedContest)
        {

            try
            {

                var contest = _contestRepo.GetContestFull(updatedContest.Id);
                var updated = Mapper.Map(updatedContest, contest);

                _contestRepo.Update(updated);
                return Ok(Mapper.Map<EditContestPostViewModel>(updated));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPost("{id}")]
        public IActionResult ChangeStatus(int id, [FromBody] ContestStatus status)
        {
            var contest = _contestRepo.Get(id);
            contest.Status = status;
            _contestRepo.Update(contest);
            return Ok();
        }
    }
}