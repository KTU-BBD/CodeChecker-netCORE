using System.Collections.Generic;
using AutoMapper;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class GymController : FrontBaseController
    {
        private readonly ContestRepository _contestRepo;

        public GymController(ContestRepository contestRepo)
        {
            _contestRepo = contestRepo;
        }

        [HttpGet("")]
        public IActionResult All([FromQuery] DataFilterViewModel filterData)
        {
            var contests = _contestRepo.GetActiveGymPagedData(filterData);
            return Ok(Mapper.Map<IEnumerable<ContestViewModel>>(contests));
        }

        [HttpGet("{contestId}")]
        public IActionResult Get(long contestId)
        {
            var contest = _contestRepo.GetGymWithAssignments(contestId);

            if (contest == null)
            {
                return BadRequest("Gym not found");
            }

            return Ok(Mapper.Map<ContestWithAssignmentViewModel>(contest));
        }
    }
}