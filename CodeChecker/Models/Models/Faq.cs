namespace CodeChecker.Models.Models
{
    public class Faq: SoftDeletable
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}