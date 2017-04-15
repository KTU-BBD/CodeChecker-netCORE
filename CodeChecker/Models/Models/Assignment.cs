using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public Contest Contest { get; set; }
        public int MemoryLimit { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool isActive { get; set; }
        public int SolvedCount { get; set; }
        public int MaxPoints { get; set; }
        public ICollection<Submission> Submissions {get;set;}
    }
}
