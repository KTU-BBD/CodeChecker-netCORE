using System.Linq;
using CodeChecker.Data;
using System.Collections.Generic;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class ApplicationUserRepository
    {
        private ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetTopUsers(int num)
        {
            return _context.Users.OrderByDescending(x => x.Rating).Take(num).ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserWithContest(ApplicationUser user)
        {
            if (user == null)
            {
                return user;
            }

            return _context.Users
                .Include(u => u.ContestParticipants)
                .Include(u => u.ContestCreators)
                .First(u => u.Id == user.Id);
        }

        public IEnumerable<ApplicationUser> GetByIds(ICollection<string> list)
        {
            return _context.Users.Where(u => list.Contains(u.Id));
        }
    }
}