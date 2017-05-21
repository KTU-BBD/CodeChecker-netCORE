using System.Collections.Generic;
using CodeChecker.Models.AssetViewModels;

namespace CodeChecker.Models.UserViewModels
{
    public class ProfileViewViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Rating { get; set; }
        public long TotalSubmissions { get; set; }
        public long SuccesfullSubmissions { get; set; }
        public long UnsuccesfullSubmissions { get; set; }
        public AssetProfileViewModel ProfileImage { get; set; }
    }
}