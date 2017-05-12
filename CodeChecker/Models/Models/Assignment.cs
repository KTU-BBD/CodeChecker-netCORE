using System.Collections.Generic;

namespace CodeChecker.Models.Models
{
    public class Assignment : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public Contest Contest { get; set; }
        public int MemoryLimit { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public ApplicationUser Creator { get; set; }
        public bool IsActive { get; set; }
        public int SolvedCount { get; set; }
        public int MaxPoints { get; set; }
        public ICollection<Input> Inputs { get; set; }
        public ICollection<Submission> Submissions { get; set; }
        public ICollection<AssignmentTag> AssignmentTags { get; set; }

        public Assignment()
        {
            InputType = "standard input";
            OutputType = "standard output";
        }
    }
}