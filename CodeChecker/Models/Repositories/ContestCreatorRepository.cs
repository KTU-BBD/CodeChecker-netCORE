using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class ContestCreatorRepository
    {
        private readonly ApplicationDbContext _context;

        public ContestCreatorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(ContestCreator contestCreator, bool autoSave = true)
        {
            _context.ContestCreators.Add(contestCreator);
            if (autoSave)
            {
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}