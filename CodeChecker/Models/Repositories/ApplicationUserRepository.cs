using System.Linq;
using CodeChecker.Data;
using System.Collections.Generic;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;
using CodeChecker.Models.ServiceViewModels;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace CodeChecker.Models.Repositories
{
    public class ApplicationUserRepository
    {
        private ApplicationDbContext _context;
        public const int MaxPerPage = 100;
        public IQueryable<ApplicationUser> _queryable;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
            _queryable = _context.Users;
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
                .First(u => u.Id == user.Id);
        }

        public IEnumerable<ApplicationUser> GetByIds(ICollection<string> list)
        {
            return _context.Users.Where(u => list.Contains(u.Id));
        }

        public IQueryable<ApplicationUser> GetPagedData(DataFilterViewModel filter)
        {
            if (filter.Page < 1)
            {
                filter.Page = 1;
            }

            if (filter.Count > GetMaxPerPage())
            {
                filter.Count = GetMaxPerPage();
            }

            var offset = filter.Count * (filter.Page - 1);

            
            foreach (var item in filter.Filter)
            {
                if (item.Key != "null" && item.Value != "null")
                {
                    var type = typeof(ApplicationUser);
                    var property = type.GetProperty(item.Key);
                    _queryable = _queryable.Where(x => x.UserName.Contains(item.Value)); 
                }
            }

            foreach (var item in filter.Sorting)
            {
                _queryable = _queryable.OrderBy(item.Key,item.Value);
            }

            return _queryable.Skip(offset).Take(filter.Count);
        }

        protected virtual int GetMaxPerPage()
        {
            return MaxPerPage;
        }

    }
}