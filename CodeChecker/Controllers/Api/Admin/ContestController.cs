using System.Linq;
using AutoMapper;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Controllers.Api.Admin
{
    public class ContestController : AdminBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly IMapper _mapper;
        private readonly ApplicationUserRepository _userRepo;
        private readonly ContestCreatorRepository _contestCreatorRepo;

        public ContestController(ContestRepository contestRepo, UserManager<ApplicationUser> userManager,
            IMapper mapper, ApplicationUserRepository userRepo, ContestCreatorRepository contestCreatorRepo)
        {
            _contestRepo = contestRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _contestCreatorRepo = contestCreatorRepo;
        }

        [HttpGet("")]
        public IActionResult All(int page = 0, int perPage = 100)
        {
            var results = _contestRepo.GetPagedData(page, perPage)
                .Include(c => c.ContestCreators)
                .ThenInclude(u => u.User)
                .First();
            return Ok(_mapper.Map<ViewContestViewModel>(results));
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreateContestViewModel contest)
        {
            if (ModelState.IsValid)
            {
                var newContest = _mapper.Map<Contest>(contest);
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
    }
}