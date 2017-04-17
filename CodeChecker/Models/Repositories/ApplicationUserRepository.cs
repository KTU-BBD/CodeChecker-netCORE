using System.Linq;
using CodeChecker.Data;
using System.Collections.Generic;
using CodeChecker.Models.Models;

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

        public List<ApplicationUser> GetTopUsers(int num)
        {
            return _context.Users.OrderByDescending(x => x.Rating).Take(num).ToList();
        }
    }
}