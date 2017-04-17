using System.Linq;
using CodeChecker.Data;
<<<<<<< HEAD
using System.Collections.Generic;
=======
using CodeChecker.Models.Models;
>>>>>>> 60e89ec293aada283f5eca63bc3e7c25cbedea92

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