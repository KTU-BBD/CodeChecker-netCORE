using System.Linq;
using CodeChecker.Data;

namespace CodeChecker.Models.Repositories
{
    public class ApplicationUserRepository
    {
        private ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string userName)
        {
            return _context.Users.First(x => x.UserName == userName);
        }
    }
}