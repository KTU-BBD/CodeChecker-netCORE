using System.Collections.Generic;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.SubmissionViewModels
{
    public class SubmissionGrouppingList
    {
        public string UserName { get; set; }
        public IList<SubmissionGroup> Submissions { get; set; }
    }
}