using System.Threading.Tasks;
using AutoMapper;
using CodeChecker.Models;
using CodeChecker.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChecker.Controllers.Api.Admin
{
    public class UserController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userViewModel = _mapper.Map<AdminPanelUserViewModel>(user);

            userViewModel.Roles = await _userManager.GetRolesAsync(user);

            return Ok(userViewModel);
        }
    }
}