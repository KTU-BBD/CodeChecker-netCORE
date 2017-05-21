using CodeChecker.Data;
using CodeChecker.Models.Models;
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

        //public override IQueryable<Faq> Query()
        //{
        //    return base.Query().Include(a => a.Creator).Where(c => c.DeletedAt == null);
        //}



    }
}
