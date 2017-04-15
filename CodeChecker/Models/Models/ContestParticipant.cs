using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class ContestParticipant
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Contest Contest { get; set; }
    }
}
