using System.Collections.Generic;
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class SubmissionGroup : BaseModel
    {
        public SubmissionVerdict Verdict { get; set; }
        public string Message { get; set; }
        public long Memory { get; set; }
        public double Time { get; set; }
        public string Language { get; set; }
        public long Points { get; set; }
        public Contest Contest { get; set; }

        public ICollection<Submission> Submissions { get; set; }

        public SubmissionGroup()
        {
            Message = "Running tests...";
            Verdict = SubmissionVerdict.Running;
        }
    }
}