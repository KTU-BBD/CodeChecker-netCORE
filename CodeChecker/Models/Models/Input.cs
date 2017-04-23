using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChecker.Models.Models
{
    public class Input : BaseModel
    {
        public string Text { get; set; }
        public Assignment Assignment { get; set; }

        public Output Output { get; set; }
    }
}