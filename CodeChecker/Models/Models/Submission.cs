using System.ComponentModel.DataAnnotations.Schema;
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class Submission : BaseModel
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [ForeignKey("AssignmentId")]
        public Assignment Assignment { get; set; }
        public long AssignmentId { get; set; }

        [ForeignKey("SubmissionGroupId")]
        public SubmissionGroup SubmissionGroup { get; set; }
        public long SubmissionGroupId { get; set; }

        public string Code { get; set; }
        public string Language { get; set; }
        public SubmissionVerdict Verdict { get; set; }
        public int TimeMs { get; set; }
        public string Output { get; set; }

        public Submission()
        {
            Verdict = SubmissionVerdict.Error;
            TimeMs = -1;
        }
    }
}