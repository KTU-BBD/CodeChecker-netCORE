using System;
using System.Collections.Generic;
using System.Linq;
using CodeChecker.Models.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChecker.Models.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public DateTime DeletedAt { get; set; }
        public Asset ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<ContestParticipant> ContestParticipants { get; set; }
        public ICollection<SubmissionGroup> SubmissionGroups { get; set; }
        public ICollection<Contest> Contests { get; set; }
        public ICollection<Article> Articles { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}