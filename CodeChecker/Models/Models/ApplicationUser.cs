using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChecker.Models.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateJoined { get; set; }
        public int Rating { get; set; }
        public Asset ProfileImage { get; set; }
    }
}
