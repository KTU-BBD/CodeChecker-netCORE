using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.ArticleViewModel
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string LongDescription { get; set; }
        public string Title { get; set; }
        public UserViewModel Creator { get; set; }
    }
}