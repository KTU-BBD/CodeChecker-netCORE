using System.Collections.Generic;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<Article> Query()
        {
            return base.Query().Include(a => a.Creator).Where(c => c.DeletedAt == null);
        }

        public IEnumerable<Article> GetPaginatedByStatus(int page, int newsPerPage, ArticleStatus status)
        {
            if (page < 0)
            {
                page = 0;
            }

            if (newsPerPage < 5)
            {
                newsPerPage = 5;
            }
            else if (newsPerPage > 20)
            {
                newsPerPage = 20;
            }

            return Query().Where(a => a.Status == status).Skip(page * newsPerPage).Take(newsPerPage);
        }
    }
}