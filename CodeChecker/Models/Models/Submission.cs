using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class Submission
    { 
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Assignment Assignment { get; set; }
        public string Language { get; set; }
        public string Verdict { get; set; }
        public int TimeMs { get; set; }
        public DateTime CreatedAt { get; set; }
        public Contest Contest { get; set; }
    }
}
