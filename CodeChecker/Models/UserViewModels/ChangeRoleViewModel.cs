using System.ComponentModel.DataAnnotations;

namespace CodeChecker.Models.UserViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }

        [RegularExpression(@"(Administrator|Moderator|Contributor)")]
        public string Role { get; set; }
    }
}