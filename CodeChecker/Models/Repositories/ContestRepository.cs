using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class ContestRepository : BaseRepository<Contest>
    {
        public ContestRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}