using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class Article: SoftDeletable
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public ArticleStatus Status { get; set; }
        public ApplicationUser Creator { get; set; }

        public Article()
        {
            Status = ArticleStatus.Unpublished;
        }
    }
}