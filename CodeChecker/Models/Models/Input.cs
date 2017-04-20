namespace CodeChecker.Models.Models
{
    public class Input : BaseModel
    {
        public string Text { get; set; }
        public Assignment Assignment { get; set; }
    }
}
