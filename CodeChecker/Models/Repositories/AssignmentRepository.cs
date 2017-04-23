using System.Linq;
using System.Linq.Dynamic.Core;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>
    {
        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Assignment GetById(long id)
        {
            return Query()
                .FirstOrDefault(a => a.Id == id);
        }
       
    }
}