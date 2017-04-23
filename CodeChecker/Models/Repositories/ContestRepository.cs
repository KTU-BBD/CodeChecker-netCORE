﻿using System;
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
                    .Where(c => c.Password != null)
                    .Where(c => c.EndAt > DateTime.Now)
                    .Where(c => c.Status == ContestStatus.Approved)
                    .OrderBy(c => c.StartAt)
                    .Include(c => c.ContestCreators)
                    .ThenInclude(c => c.User)
                    .Include(c => c.ContestParticipants)
                ;
        }
    }
}