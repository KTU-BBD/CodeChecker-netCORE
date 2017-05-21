using System;
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
            return _context.Users
                .Include(u => u.ProfileImage)
                .FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserWithContest(ApplicationUser user)
        {
            return _context.Users
                .Include(u => u.ContestParticipants)
                .FirstOrDefault(u => u.Id == user.Id);
        }

        public IEnumerable<ApplicationUser> GetByIds(ICollection<string> list)
        {
            return _context.Users.Where(u => list.Contains(u.Id));
        }

        /// <summary>
        /// Returns User which has given email or username but is not equal to given userId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser GetByUsernameOrEmail(string userId, string username, string email)
        {
            return _context.Users.FirstOrDefault(u => (u.UserName.Equals(username) || u.Email.Equals(email)) &&
                                                      u.Id != userId);
        }

        public ApplicationUser GetByUsername(string username)
        {
            return _context.Users.Include(u => u.UserStatistics).Include(u => u.ProfileImage).Include(u => u.SubmissionGroups).FirstOrDefault(u => u.UserName.Equals(username));
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
            var queryable = _context.Users.AsQueryable();
            foreach (var item in filter.Filter)
            {
                if (item.Key != "null" && item.Value != "null")
                {
                    var type = typeof(ApplicationUser);
                    try
                    {
                        var property = type.GetProperty(Decode(item.Key));

                        if (property.ToString().Contains(typeof(long).Name))
                        {
                            queryable = queryable.Where($"{Decode(item.Key)} = {Decode(item.Value)}");
                        }
                        else if (property.ToString().Contains(typeof(string).Name))
                        {
                            queryable = queryable.Where($"{Decode(item.Key)}.Contains(@0)", Decode(item.Value));
                        }
                    }
                    catch (Exception ex)
                    {
                        queryable = queryable.Where($"{Decode(item.Key)}.Contains(@0)", Decode(item.Value));
                    }
                }
            }

            foreach (var item in filter.Sorting)
            {
                queryable = queryable.AsQueryable().OrderBy($"{Decode(item.Key)} {Decode(item.Value)}");
            }

            return queryable.Skip(offset).Take(filter.Count);
        }

        protected virtual int GetMaxPerPage()
        {
            return MaxPerPage;
        }

        private static string Decode(string value)
        {
            return System.Net.WebUtility.UrlDecode(value);
        }
    }
}