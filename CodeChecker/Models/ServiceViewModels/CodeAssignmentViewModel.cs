using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.ServiceViewModels
{
    public class CodeAssignmentViewModel
    {
        public AssignmentSubmitViewModel AssignmentSubmit { get; set; }
        public long AssignmentId { get; set; }
        public string SubmiterId { get; set; }
    }
}