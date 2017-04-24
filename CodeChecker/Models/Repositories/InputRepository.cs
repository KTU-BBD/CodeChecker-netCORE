using System.Linq;
using System.Linq.Dynamic.Core;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class InputRepository : BaseRepository<Input>
    {
        public InputRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Input GetById(long id)
        {
            return Query()
                .FirstOrDefault(a => a.Id == id);
        }
        public Input GetByIdWithOutput(long id)
        {
            return Query()
                .Include(a => a.Output)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
