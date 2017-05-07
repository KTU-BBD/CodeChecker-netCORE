using AutoMapper;
using CodeChecker.Models;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CodeChecker.Controllers.Api.Front
{
    public class UserController : FrontBaseController
    {
        private readonly ApplicationUserRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController( ApplicationUserRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet("{num}")]
        public IActionResult GetTopUsers(int num)
        {
            var personViews =
                Mapper.Map<IEnumerable<TopUserViewModel>>(_repository.GetTopUsers(num));
            return Ok(personViews);
        }

        [HttpGet]
        public async Task<IActionResult> Current()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = _repository.GetById(currentUser.Id);

            var userViewModel = Mapper.Map<AdminPanelUserViewModel>(user);

            userViewModel.Roles = await _userManager.GetRolesAsync(currentUser);

            return Ok(userViewModel);
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var results = _repository.GetByUsername(username);

            if (results == null)
            {
                return BadRequest("User not found");
            }

            return Ok(Mapper.Map<UserProfileViewModel>(results));
        }
    }
}