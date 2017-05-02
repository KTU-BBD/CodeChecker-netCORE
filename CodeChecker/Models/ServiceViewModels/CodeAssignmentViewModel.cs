using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.ServiceViewModels
{
    public class CodeAssignmentViewModel
    {
        public AssignmentSubmitViewModel AssignmentSubmit { get; set; }
        public Assignment Assignment { get; set; }
        public Contest Contest { get; set; }
        public ApplicationUser Submiter { get; set; }
    }
}