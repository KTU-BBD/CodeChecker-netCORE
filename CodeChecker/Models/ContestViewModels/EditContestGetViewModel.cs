using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.ContestViewModels
{
    public class EditContestGetViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SuccessfulSubmit { get; set; }
        public int UnsuccessfulSubmit { get; set; }
        public ContestStatus Status { get; set; }
        public string Password { get; set; }
        public ICollection<ShortAssignmentViewModel> Assignments { get; set; }
        public UserViewModel Creator { get; set; }
    }
}
