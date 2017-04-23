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
        public string Language { get; set; }
    }
}