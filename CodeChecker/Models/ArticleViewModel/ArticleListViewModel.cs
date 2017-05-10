using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.ArticleViewModel
{
    public class ArticleListViewModel
    {
        public long Id { get; set; }
        public string ShortDescription { get; set; }
        public UserViewModel Creator { get; set; }
    }
}