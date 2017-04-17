using AutoMapper;
using CodeChecker.Models;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Front
{
    public class UserController : FrontBaseController
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private ApplicationUserRepository _repository;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationUserRepository repository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{num}")]
        public IActionResult GetTopUsers(int num)
        {
            return Ok(_repository.GetTopUsers(num));
        }
    }
}