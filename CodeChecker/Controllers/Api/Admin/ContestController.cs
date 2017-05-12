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
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace CodeChecker.Controllers.Api.Admin
{
    public class ContestController : AdminBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly ApplicationUserRepository _userRepo;
        private UserManager<ApplicationUser> _userManager;


        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            ApplicationUserRepository userRepo)
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
                var contests = _contestRepo.GetPagedDataIncludeDeleted(filterData);
                if (contests != null)
                return Ok(contests);
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                var query = _contestRepo.QueryDeleted().Where(c => c.Creator.Id == userId);

                var contests = _contestRepo.GetPagedData(query, filterData);

                if (contests != null)
                {
                    return Ok(contests);
                }
            }

            return BadRequest();
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreateContestViewModel contest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newContest = Mapper.Map<Contest>(contest);

                    var assignedUser = _userRepo.GetById(_userManager.GetUserId(User));
                    newContest.Creator = assignedUser;
                    _contestRepo.Insert(newContest);
                    return Ok(newContest.Id);
                }
                return BadRequest("Name or password are not valid");
            }
            catch (Exception ex) {

                return BadRequest("Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var contest = _contestRepo.Get(id);
                if (User.IsInRole("Contributor") && contest.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    return Ok(contest);
                }
                else {
                    return BadRequest("Unauthorized");
                }
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
                if (User.IsInRole("Contributor") && contest.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    contest.Assignments = contest.Assignments.OrderByDescending(x => x.Id).ToList();
                    var contestToReturn = Mapper.Map<EditContestGetViewModel>(contest);
                    return Ok(contestToReturn);
                }
                else {
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
       
        [HttpPost]
        public IActionResult Update([FromBody] EditContestPostViewModel updatedContest)
        { 
            try
            {
                if (ModelState.IsValid)
                {
                    var contest = _contestRepo.GetContestFull(updatedContest.Id);
                    if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                    {
                        var updated = Mapper.Map(updatedContest, contest);
                        updated.Status = contest.Status;
                        updated.UpdatedAt = DateTime.Now;
                        _contestRepo.Update(updated);
                        return Ok("Updated");
                    }
                            
                    if (User.IsInRole("Contributor") && contest.Creator.Id == _userManager.GetUserId(User))
                    {
                        if (contest.Status == ContestStatus.Submited || contest.Status == ContestStatus.Approved)
                        {
                            return BadRequest("You are not allowed to edit contest after submission");
                        }
                        var updated = Mapper.Map(updatedContest, contest);
                        updated.Status = contest.Status;
                        updated.UpdatedAt = DateTime.Now;
                        _contestRepo.Update(updated);
                        return Ok("Updated");
                    }
                }
                else {
                    return BadRequest("Bad contest data");
                }
                    return BadRequest("Unauthorized");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("{id}")]
        public IActionResult ChangeStatus(int id, [FromBody] ContestStatus status)
        {
            try
            {
                var article = _contestRepo.Get(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    article.Status = status;
                    _contestRepo.Update(article);
                    return Ok("Status changed");
                }
                if (User.IsInRole("Contributor") && article.Creator.Id == _userManager.GetUserId(User))
                {
                    if (article.Status == ContestStatus.Submited || article.Status == ContestStatus.Approved)
                    {
                        return BadRequest("You are not allowed to change status after submission");
                    }
                    article.Status = status;
                    _contestRepo.Update(article);
                    return Ok("Status changed");
                }
                return BadRequest("Unauthorized");
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost("{id}")]
        public IActionResult DeleteContest(int id)
        {
            try
            {
                var contest = _contestRepo.GetContestFull(id);
                if (User.IsInRole("Contributor") && contest.Creator.Id == _userManager.GetUserId(User) || User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _contestRepo.Delete(contest);
                    return Ok("Contest deleted");
                }
                else {
                    return BadRequest("Unauthorized");
                }
            } catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }
        
        [HttpPost("{id}")]
        public IActionResult RecoverContest(int id)
        {
            try
            {
                var contest = _contestRepo.GetContestFullWithDeleted(id);
                if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
                {
                    _contestRepo.Recover(contest);
                    _contestRepo.ResetStatus(contest);
                    return Ok("Contest recovered");
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