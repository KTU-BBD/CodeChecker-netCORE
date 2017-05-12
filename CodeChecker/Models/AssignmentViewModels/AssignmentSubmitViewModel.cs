using System.ComponentModel.DataAnnotations;

namespace CodeChecker.Models.AssignmentViewModels
{
    public class AssignmentSubmitViewModel
    {
        [Required]
        public long AssignmentId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [RegularExpression(@"(PYT27|PYT3|CSH|CPP5.4.0)")]
        public string Language { get; set; }
    }
}