using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChecker.Models.Models
{
    public class ContestCreator
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Contest")]
        public long ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}