using CodeChecker.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.ArticleViewModel
{
    public class ArticleAdminListViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public UserViewModel Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
