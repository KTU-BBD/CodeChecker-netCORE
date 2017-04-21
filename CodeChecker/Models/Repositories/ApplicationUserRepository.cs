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

        public List<ApplicationUser> GetTopUsers(int num)
        {
            return _context.Users.OrderByDescending(x => x.Rating).Take(num).ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ApplicationUser> GetByIds(ICollection<string> list)
        {
            return _context.Users.Where(u => list.Contains(u.Id));
        }
    }
}