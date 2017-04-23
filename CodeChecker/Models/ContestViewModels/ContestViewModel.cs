using System;
using System.Collections.Generic;

namespace CodeChecker.Models.ContestViewModels
{
    public class ContestViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public IList<ContestContributorViewModel> ContestCreators { get; set; }
        public bool Joined { get; set; }
    }
}