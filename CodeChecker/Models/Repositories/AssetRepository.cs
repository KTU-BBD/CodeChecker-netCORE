using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class AssetRepository : BaseRepository<Asset>
    {
        public AssetRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}