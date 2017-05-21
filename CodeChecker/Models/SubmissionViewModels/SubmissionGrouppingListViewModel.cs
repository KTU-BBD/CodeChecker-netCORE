using System.Collections.Generic;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.SubmissionViewModels
{
    public class SubmissionGrouppingListViewModel
    {
        public string UserName { get; set; }
        public IList<SubmissionViewModel> Submissions { get; set; }
    }
}