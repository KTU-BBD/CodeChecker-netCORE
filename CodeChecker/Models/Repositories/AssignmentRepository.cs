using System;
using System.Linq;
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

        public override IQueryable<Assignment> Query()
        {
            return base.Query()
                    .Include(c => c.Contest)
                    .Where(c => c.Contest.DeletedAt == null && c.Contest.StartAt >= DateTime.Now)
                ;
        }

        public Assignment GetById(long id)
        {
            return Query()
                .FirstOrDefault(a => a.Id == id);
        }

        public Assignment GetByIdWithInputsOutputs(long id)
        {
            return Query()
                .Include(a => a.Inputs)
                .ThenInclude(a => a.Output)
                .Include(a => a.Creator)
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

        public Assignment GetAssignmentFull(long assignmentId)
        {
            return Query()
                    .Include(a => a.Contest)
                    .ThenInclude(a => a.ContestParticipants)
                    .ThenInclude(a => a.User)
                    .Include(a => a.Creator)
                    .Include(a => a.Inputs)
                    .ThenInclude(a => a.Output)
                    .Include(a => a.Submissions)
                    .FirstOrDefault(c => c.Id == assignmentId)
                ;
        }

        public void CreateTestForAssignment(long assignID)
        {
            var assign = GetById(assignID);
            var outp = new Output();
            var inp = new Input();
            inp.Output = outp;
            inp.Assignment = assign;
            outp.Input = inp;
            assign.Inputs.Add(inp);
            Update(assign);
        }
    }
}