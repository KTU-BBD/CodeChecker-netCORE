using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChecker.Models.Models
{
    public class AssignmentTag
    {
        [ForeignKey("Assignment")]
        public long AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        [ForeignKey("Tag")]
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
