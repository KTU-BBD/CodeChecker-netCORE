using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.ArticleViewModels
{
    public class ChangeArticleStatusWMessage
    {
        public ArticleStatus Status { get; set; }
        public string Message { get; set; }
    }
}
