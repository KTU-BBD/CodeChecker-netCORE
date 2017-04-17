using System.Collections.Generic;
using System.ComponentModel;
using CodeChecker.Models.AssetViewModels;

namespace CodeChecker.Models.AccountViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public AssetProfileViewModel ProfileImage { get; set; }
    }
}