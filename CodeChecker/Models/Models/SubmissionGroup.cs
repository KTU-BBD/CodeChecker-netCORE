using System.Collections.Generic;
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class SubmissionGroup : BaseModel
    {
        public SubmissionVerdict Verdict { get; set; }
        public string Message { get; set; }

        public ICollection<Submission> Submissions { get; set; }

        public SubmissionGroup()
        {
            Message = "Running tests...";
            Verdict = SubmissionVerdict.Running;
        }
    }
}