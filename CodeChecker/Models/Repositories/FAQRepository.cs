using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Repositories
{
    public class FAQRepository : BaseRepository<Faq>
    {
        public FAQRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Faq> GetPagedDataIncludeDeleted(DataFilterViewModel filter)
        {
            return GetPagedData(entities, filter).Include(c => c.Creator);
        }

        public IQueryable<Faq> QueryDeleted()
        {
            return base.Query();
        }

        public IQueryable<Faq> QueryDeletedWithCreator()
        {
            return base.Query()
                    .Include(c => c.Creator)
                ;
        }

        public Faq GetFaqFull(long id)
        {
            return Query()
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == id)
                ;
        }

        public Faq GetArticleFullWithDeleted(long id)
        {
            return base.Query()
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == id)
                ;
        }


        

    }
}
