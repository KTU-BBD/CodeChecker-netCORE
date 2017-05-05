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

namespace CodeChecker.Controllers.Api.Admin
{
    public class UserController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FileUploadService _uploadService;
        private readonly ApplicationDbContext _context;
        private readonly AssetRepository _assetRepo;
        private readonly ApplicationUserRepository _userRepo;

        public UserController(UserManager<ApplicationUser> userManager, FileUploadService uploadService, ApplicationDbContext context, AssetRepository assetRepo, ApplicationUserRepository userRepo)
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

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(IFormFile file)
        {
            var result = await _uploadService.SavePicture(file);
            if (result != null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.ProfileImage = result;
                await _userManager.UpdateAsync(user);

                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        public IActionResult AllPaged([FromQuery] DataFilterViewModel filterData)
        {
            var users = _userRepo.GetPagedData(filterData);
            if (users != null)
                return Ok(users);
            else
                return BadRequest();
        }
    }
}