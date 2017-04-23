using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class SubmissionRepository: BaseRepository<Submission>
    {
        public SubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}