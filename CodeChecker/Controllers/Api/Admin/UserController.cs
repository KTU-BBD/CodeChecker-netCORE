using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Data;
using CodeChecker.Models.AccountViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Repositories;
using CodeChecker.Services.FileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Controllers.Api.Admin
{
    public class UserController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FileUploadService _uploadService;
        private readonly ApplicationDbContext _context;
        private readonly AssetRepository _assetRepo;
        private readonly ApplicationUserRepository _userRepo;

        public UserController(UserManager<ApplicationUser> userManager, FileUploadService uploadService,
            ApplicationDbContext context, AssetRepository assetRepo, ApplicationUserRepository userRepo)
        {
            _userManager = userManager;
            _uploadService = uploadService;
            _context = context;
            _assetRepo = assetRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Current()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userr = _context.Users.Include(u => u.ProfileImage).First(u => u.Id == user.Id);
            var userViewModel = Mapper.Map<AdminPanelUserViewModel>(userr);

            userViewModel.Roles = await _userManager.GetRolesAsync(user);

            return Ok(userViewModel);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (!User.IsInRole("Administrator") && currentUser.Id != userId)
            {
                return BadRequest("No persmissions");
            }

            var user = _context.Users
                .Include(u => u.ProfileImage)
                .First(u => u.Id == userId);

            if (user != null)
            {
                var userViewModel = Mapper.Map<AdminPanelUserViewModel>(user);
                userViewModel.Roles = await _userManager.GetRolesAsync(user);

                return Ok(userViewModel);
            }

            return BadRequest("User does not exist");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLock([FromBody] UserLockViewModel lockViewModel)
        {
            if (!User.IsInRole("Administrator"))
            {
                return BadRequest("Cannot update this user profile");
            }
            var user = await _userManager.FindByIdAsync(lockViewModel.UserId);

            if (lockViewModel.Lock)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddYears(10));
                await _userManager.UpdateSecurityStampAsync(user);

                return Ok($"Locked user {user.UserName}");
            }

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now);
            await _userManager.SetLockoutEnabledAsync(user, false);

            return Ok($"Unlocked user {user.UserName}");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(ProfileUpdateViewModel profileUpdate)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (!User.IsInRole("Administrator") && currentUser.Id != profileUpdate.UserId)
            {
                return BadRequest("Cannot update this user profile");
            }

            var result = await _uploadService.SavePicture(profileUpdate.Picture);
            var user = await _userManager.FindByIdAsync(profileUpdate.UserId);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (result != null)
            {
                user.ProfileImage = result;
            }

            Mapper.Map(profileUpdate, user);

            var userCheck = _userRepo.GetByUsernameOrEmail(user.Id, profileUpdate.UserName, profileUpdate.Email);
            if (userCheck != null)
            {
                if (userCheck.Email.Equals(profileUpdate.Email))
                {
                    return BadRequest("Email is already in use");
                }

                return BadRequest("Username is already in use");
            }

            await _userManager.UpdateAsync(user);

            return Ok("Profile updated successfully");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole([FromBody]ChangeRoleViewModel roleData)
        {
            if (!User.IsInRole("Administrator"))
            {
                return BadRequest("No persmissions");
            }

            if (roleData.Role == "Administrator")
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser.Id == roleData.UserId)
                {
                    return BadRequest("Cannot remove administrator role from yourself");
                }
            }

            var user = _userRepo.GetById(roleData.UserId);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (await _userManager.IsInRoleAsync(user, roleData.Role))
            {
                await _userManager.RemoveFromRoleAsync(user, roleData.Role);
                return Ok($"Removed role {roleData.Role} from user {user.UserName}");
            }

            await _userManager.AddToRoleAsync(user, roleData.Role);
            return Ok($"Added role '{roleData.Role}' to user {user.UserName}");
        }

        [HttpGet]
        public async Task<IActionResult> AllPaged([FromQuery] DataFilterViewModel filterData)
        {
            if (!User.IsInRole("Administrator"))
            {
                return BadRequest("No persmissions");
            }

            var users = _userRepo.GetPagedData(filterData);
            if (users != null)
                return Ok(users);

            return BadRequest();
        }
    }
}