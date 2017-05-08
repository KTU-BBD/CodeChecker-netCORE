using System.Collections.Generic;

namespace CodeChecker.Models.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }
        public ICollection<AssignmentTag> AssignmentTags { get; set; }
    }
}
