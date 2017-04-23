using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChecker.Models.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public DateTime DeletedAt { get; set; }
        public Asset ProfileImage { get; set; }
        public ICollection<ContestCreator> ContestCreators { get; set; }
        public ICollection<ContestParticipant> ContestParticipants { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}