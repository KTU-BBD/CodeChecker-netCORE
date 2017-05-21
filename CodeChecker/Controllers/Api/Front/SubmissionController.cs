using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.SubmissionViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class SubmissionController : FrontBaseController
    {
        private readonly ContestRepository _contestRepo;
        private readonly SubmissionGroupRepository _groupRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubmissionController(ContestRepository contestRepo, SubmissionGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _contestRepo = contestRepo;
            _groupRepository = groupRepository;
            _userManager = userManager;
        }

        [HttpGet("{contestId}")]
        public ActionResult GetByContest(long contestId)
        {
            var contest = _contestRepo.GetWithAssignments(contestId);

            if (contest == null)
            {
                return NotFound();
            }

            return Ok(contest);
        }

        [HttpGet("{submissionId}")]
        public ActionResult Get(long submissionId)
        {
            var submission = _groupRepository.GetWithSubmitee(submissionId);

            if (submission == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SubmissionViewModel>(submission));
        }

        [HttpGet("{contestId}")]
        public async Task<ActionResult> ContestSubmssions(long contestId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return BadRequest("You need to login first");
            }

            var contest = _contestRepo.Get(contestId);

            if (contest == null)
            {
                return BadRequest("Contest not found");
            }


            return Ok(Mapper.Map<IEnumerable<SubmissionViewModel>>(_groupRepository.GetByContestAndUser(contest, currentUser)));
        }

        [HttpGet]
        public ActionResult All()
        {
            return Ok(Mapper.Map<IEnumerable<SubmissionViewModel>>(_groupRepository.GetLatest(50)));
        }
    }
}