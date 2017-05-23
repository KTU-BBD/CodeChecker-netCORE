namespace CodeChecker.Models.Models
{
    public class Faq : BaseModel
    {
        public string Question { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}