using System;
using System.Collections.Generic;
using CodeChecker.Models.AssignmentViewModels;

namespace CodeChecker.Models.ContestViewModels
{
    public class ContestWithAssignmentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public IList<ShortAssignmentViewModel> Assignments { get; set; }
    }
}