using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class ContestParticipantRepository
    {
        private readonly ApplicationDbContext _context;

        public ContestParticipantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(ContestParticipant participant, bool autoSave = true)
        {
            _context.ContestParticipants.Add(participant);

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