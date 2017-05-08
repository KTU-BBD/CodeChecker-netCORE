using System;
using System.Collections.Generic;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using CodeChecker.Models.ServiceViewModels;

namespace CodeChecker.Models.Repositories
{
    public class ContestRepository : BaseRepository<Contest>
    {
        public ContestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Contest> GetActiveContestPagedData(DataFilterViewModel filterData)
        {
            return GetPagedData(filterData)
                .Include(c => c.Creator)
                .Include(c => c.ContestParticipants);
        }

        public override IQueryable<Contest> Query()
        {
            return base.Query().Where(c => c.DeletedAt == null);
        }

        public IQueryable<Contest> QueryDeleted()
        {
            return base.Query();
        }

        public IQueryable<Contest> GetPagedDataIncludeDeleted(DataFilterViewModel filter)
        {
            return GetPagedData(entities, filter);
        }

        private IQueryable<Contest> ActiveContests(IQueryable<Contest> queryable)
        {
            return queryable.Where(c => c.EndAt > DateTime.Now)
                    .Where(c => c.Status == ContestStatus.Approved)
                    .OrderBy(c => c.StartAt)
                    .Include(c => c.Creator)
                    .Include(c => c.ContestParticipants)
                ;
        }

        public Contest GetContestWithAssignments(long contestId)
        {
            return Query()
                    .Include(c => c.ContestParticipants)
                    .Include(c => c.Assignments)
                    .FirstOrDefault(c => c.Id == contestId)
                ;
        }

        public Contest GetContestFull(long contestId)
        {
            return Query()
                    .Include(c => c.ContestParticipants)
                    .Include(c => c.Assignments)
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == contestId)
                ;
        }

        public Contest GetContestFullWithDeleted(long contestId)
        {
            return QueryDeleted()
                    .Include(c => c.ContestParticipants)
                    .Include(c => c.Assignments)
                    .Include(c => c.Creator)
                    .FirstOrDefault(c => c.Id == contestId)
                ;
        }

        public void ResetStatus(Contest contest)
        {
            var cont = GetContestFullWithDeleted(contest.Id);
            cont.Status = 0;
            Update(cont);
        }
    }
}