using System.ComponentModel.DataAnnotations;

namespace CodeChecker.Models.ContactViewModel
{
    public class ContactSubmitViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Message { get; set; }
    }
}