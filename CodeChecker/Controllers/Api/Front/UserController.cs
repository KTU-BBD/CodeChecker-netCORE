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
using System.Linq;
using System.Threading.Tasks;
using CodeChecker.Services.FileUpload;
using Microsoft.AspNetCore.Authorization;

namespace CodeChecker.Controllers.Api.Front
{
    public class UserController : FrontBaseController
    {
        private readonly ApplicationUserRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FileUploadService _uploadService;
        private readonly ApplicationUserRepository _userRepo;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController( ApplicationUserRepository repository, UserManager<ApplicationUser> userManager, FileUploadService uploadService, ApplicationUserRepository userRepo, SignInManager<ApplicationUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _uploadService = uploadService;
            _userRepo = userRepo;
            _signInManager = signInManager;
        }

        [HttpGet("{num}")]
        public IActionResult GetTopUsers(int num)
        {
            var personViews =
                Mapper.Map<IEnumerable<TopUserViewModel>>(_repository.GetTopUsers(num));
            return Ok(personViews);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Current()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = _userRepo.GetById(currentUser.Id);
            var userViewModel = Mapper.Map<PersonalProfileViewModel>(user);

            return Ok(userViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] PersonalProfilePasswordViewModel requestData)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        return BadRequest(error.ErrorMessage);
                    }
                }
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var results = await _userManager.ChangePasswordAsync(currentUser, requestData.CurrentPassword, requestData.Password);

            if (results.Succeeded)
            {
                await _signInManager.SignInAsync(currentUser, true);
                return Ok("Password changed successfully");
            }

            foreach (var error in results.Errors)
            {
                return BadRequest(error.Description);
            }

            return BadRequest("Password cannot be changed");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(PersonalProfileUpdateViewModel userUpdateData)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (!ModelState.IsValid)
            {
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        return BadRequest(error.ErrorMessage);
                    }
                }
            }


            Mapper.Map(userUpdateData, currentUser);

            var result = await _uploadService.SavePicture(userUpdateData.Picture);

            if (result != null)
            {
                currentUser.ProfileImage = result;
            }

            var userCheck = _userRepo.GetByUsernameOrEmail(currentUser.Id, userUpdateData.UserName, userUpdateData.Email);
            if (userCheck != null)
            {
                if (userCheck.Email.Equals(userUpdateData.Email))
                {
                    return BadRequest("Email is already in use");
                }

                return BadRequest("Username is already in use");
            }

            await _userManager.UpdateAsync(currentUser);

            return Ok("Profile updated successfully");
        }


        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var results = _repository.GetByUsername(username);

            if (results == null)
            {
                return BadRequest("User not found");
            }

            return Ok(Mapper.Map<ProfileViewViewModel>(results));
        }

        [HttpGet("{username}")]
        public IActionResult Statistics(string username)
        {
            var results = _repository.GetByUsername(username);

            if (results == null)
            {
                return BadRequest("User not found");
            }
            return Ok(Mapper.Map<IEnumerable<UserStatisticViewModel>>(results.UserStatistics.OrderBy(c => c.CreatedAt).Take(31)));
        }
    }
}