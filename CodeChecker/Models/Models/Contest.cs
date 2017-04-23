using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class Contest : SoftDeletable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int SuccessfulSubmit { get; set; }
        public int UnsuccessfulSubmit { get; set; }
        public ContestStatus Status { get; set; }
        public string Password { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<ContestParticipant> ContestParticipants { get; set; }
        public ApplicationUser Creator { get; set; }

        [NotMapped]
        public bool Joined { get; set; }

        public Contest()
        {
            Status = ContestStatus.Created;
        }
    }
}