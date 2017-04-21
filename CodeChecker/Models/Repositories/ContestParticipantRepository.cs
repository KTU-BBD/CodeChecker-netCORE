using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class ContestParticipantRepository : BaseRepository<Contest>
    {
        public ContestParticipantRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}