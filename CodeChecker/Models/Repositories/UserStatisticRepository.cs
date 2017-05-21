using System;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class UserStatisticRepository
    {
        private ApplicationDbContext _dbContext;

        public UserStatisticRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public bool Update()
        {
            var date = DateTime.Now;

            _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var applicationUser in _dbContext.Users)
                {
                    _dbContext.Add(new UserStatistic()
                    {
                        CreatedAt = date,
                        Rating = applicationUser.Rating,
                        User = applicationUser
                    });
                }

                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch
            {
                _dbContext.Database.RollbackTransaction();
                return false;
            }

            return true;
        }
    }
}