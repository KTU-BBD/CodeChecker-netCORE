using System;
using System.Collections.Generic;
using System.Linq;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using CodeChecker.Models.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class ContestRepository : BaseRepository<Contest>
    {
        public ContestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Contest> GetActiveContests()
        {
            return Query()
                    .Where(c => c.EndAt > DateTime.Now)
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
    }
}