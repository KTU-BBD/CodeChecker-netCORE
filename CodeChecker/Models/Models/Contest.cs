using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class Contest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartAt { get; set; }
        public int Duration { get; set; }
        public int SuccessfulSubmit { get; set; }
        public int UnsuccessfulSubmit { get; set; }
        public string Status { get; set; }
        public bool IsPublic { get; set; }
        public string Password { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
