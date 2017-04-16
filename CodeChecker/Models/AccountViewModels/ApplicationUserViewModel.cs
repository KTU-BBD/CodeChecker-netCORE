using System.Collections.Generic;

namespace CodeChecker.Models.AccountViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}