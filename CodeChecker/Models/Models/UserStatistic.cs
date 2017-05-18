using System;

namespace CodeChecker.Models.Models
{
    public class UserStatistic
    {
        public long Id { get; set; }
        public ApplicationUser User { get; set; }
        public long Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserStatistic()
        {
            CreatedAt = new DateTime();
        }
    }
}