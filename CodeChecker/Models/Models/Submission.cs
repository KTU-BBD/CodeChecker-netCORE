namespace CodeChecker.Models.Models
{
    public class Submission : BaseModel
    {
        public ApplicationUser User { get; set; }
        public Assignment Assignment { get; set; }
        public string Language { get; set; }
        public string Verdict { get; set; }
        public int TimeMs { get; set; }
        public Contest Contest { get; set; }
    }
}