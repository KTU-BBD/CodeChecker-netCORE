using CodeChecker.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.ArticleViewModel
{
    public class EditArticlePostViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LongDescription { get; set; }
        public ArticleStatus Status { get; set; }
        public string ShortDescription { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
