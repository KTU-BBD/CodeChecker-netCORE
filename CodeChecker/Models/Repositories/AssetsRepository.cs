using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class AssetsRepository : BaseRepository<Asset>
    {
        public AssetsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}