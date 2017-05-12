using System.Collections.Generic;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using Microsoft.EntityFrameworkCore;
using CodeChecker.Models.ServiceViewModels;

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


        public IQueryable<Article> GetPagedDataIncludeDeleted(DataFilterViewModel filter)
        {
            return GetPagedData(entities, filter).Include(c => c.Creator);
        }

        public IQueryable<Article> QueryDeleted()
        {
            return base.Query();
        }

        public IQueryable<Article> QueryDeletedWithCreator()
        {
            return base.Query()
                    .Include(c => c.Creator)
                ;
        }

        public Article GetArticleFull(long id)
        {
            return Query()
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == id)
                ;
        }

        public Article GetArticleFullWithDeleted(long id)
        {
            return base.Query()
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == id)
                ;
        }


        public void ResetStatus(Article article)
        {
            var cont = GetArticleFullWithDeleted(article.Id);
            cont.Status = 0;
            Update(cont);
        }

    }
}