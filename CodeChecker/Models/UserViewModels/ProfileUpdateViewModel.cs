using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CodeChecker.Models.UserViewModels
{
    public class ProfileUpdateViewModel
    {
        public string UserId { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Picture { get; set; }
    }
}