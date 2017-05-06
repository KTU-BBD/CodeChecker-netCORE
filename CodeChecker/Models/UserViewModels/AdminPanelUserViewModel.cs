using System.Collections.Generic;
using CodeChecker.Models.AssetViewModels;

namespace CodeChecker.Models.UserViewModels
{
    public class AdminPanelUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LockoutEnabled { get; set; }
        public IList<string> Roles { get; set; }
        public AssetProfileViewModel ProfileImage { get; set; }
    }
}