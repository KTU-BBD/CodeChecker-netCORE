using CodeChecker.Data;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
