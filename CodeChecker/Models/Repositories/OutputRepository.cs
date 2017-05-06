using System.Linq;
using System.Linq.Dynamic.Core;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class OutputRepository : BaseRepository<Output>
    {
        public OutputRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Output GetById(long id)
        {
            return Query()
                .FirstOrDefault(a => a.Id == id);
        }
        public Output GetByIdWithInput(long id)
        {
            return Query()
                .Include(a => a.Input)
                .FirstOrDefault(a => a.Id == id);
        }

    }
}
