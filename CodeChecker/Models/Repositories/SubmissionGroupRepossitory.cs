using System;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.SubmissionViewModels;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class SubmissionGroupRepository : BaseRepository<SubmissionGroup>
    {
        public SubmissionGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SubmissionGroup GetWithSubmitee(long id)
        {
            return Query().Include(c => c.Submitee).Include(c => c.Assignment).FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<SubmissionGroup> GetLatest(int count)
        {
            return Query()
                .Include(s => s.Submitee)
                .Include(s => s.Assignment)
                .ThenInclude(s => s.Contest)
                .Where(s => s.Assignment.Contest.EndAt < DateTime.Now || s.Assignment.Contest.Type == ContestType.Gym)
                .OrderByDescending(s => s.CreatedAt)
                .Take(count);
        }

        public IQueryable<SubmissionGroup> GetByContestAndUser(Contest contest, ApplicationUser user, int count = 50)
        {
            return Query()
                .Include(s => s.Submitee)
                .Include(s => s.Assignment)
                .ThenInclude(s => s.Contest)
                .Where(s => s.Assignment.Contest == contest && s.Submitee == user)
                .OrderByDescending(s => s.CreatedAt)
                .Take(count);
        }


        public SubmissionGroup GetLastUserSubmissionInContest(ApplicationUser user, Assignment assignment)
        {
            return Query()
                .Include(s => s.Assignment)
                .Include(s => s.Submitee)
                .OrderByDescending(s => s.Id)
                .FirstOrDefault(s => s.Assignment.Id == assignment.Id && s.Submitee.Id == user.Id);
        }

        public IQueryable<SubmissionGrouppingList> GetByContest(Contest contest)
        {
            return Query()
                    .Include(c => c.Assignment)
                    .ThenInclude(c => c.Contest)
                    .Where(c => c.Assignment.Contest.Id == contest.Id && c.Verdict == SubmissionVerdict.Success &&
                                c.CreatedAt < c.Assignment.Contest.EndAt && c.CreatedAt > c.Assignment.Contest.StartAt)
                    .GroupBy(c => c.Submitee)
                    .Select(g => new SubmissionGrouppingList()
                    {
                        UserName = g.Key.UserName,
                        Submissions = g.ToList()
                    })
                ;
        }
    }
}