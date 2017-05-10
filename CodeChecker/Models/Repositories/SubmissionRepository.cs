using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>
    {
        public SubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Submission GetLastUserSubmissionInContest(ApplicationUser user, Assignment assignment)
        {
            return Query()
                .OrderByDescending(s => s.Id)
                .FirstOrDefault(s => s.AssignmentId == assignment.Id && s.UserId == user.Id);
        }
    }
}