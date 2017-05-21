using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.SubmissionViewModels
{
    public class SubmissionViewModel
    {
        public long Id { get; set; }
        public SubmissionVerdict Verdict { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public long Memory { get; set; }
        public string Language { get; set; }
        public double Time { get; set; }
        public UserViewModel Submitee { get; set; }
        public ShortAssignmentViewModel Assignment { get; set; }
    }
}