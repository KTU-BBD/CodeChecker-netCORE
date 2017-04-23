﻿using System.Linq;
using System.Linq.Dynamic.Core;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChecker.Models.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>
    {
        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Assignment GetByIdWithInputsOutputs(long id)
        {
            return Query()
                .Include(a => a.Inputs)
                .ThenInclude(a => a.Output)
                .FirstOrDefault(a => a.Id == id);
        }

        public Assignment GetByIdWithContest(long id)
        {
            return Query()
                    .Include(a => a.Contest)
                    .ThenInclude(a => a.ContestParticipants)
                    .ThenInclude(a => a.User)
                    .FirstOrDefault(a => a.Id == id)
                ;
        }
    }
}