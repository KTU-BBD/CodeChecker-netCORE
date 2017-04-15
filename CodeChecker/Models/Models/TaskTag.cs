using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class TaskTag
    {
        public int Id { get; set; }
        public Assignment Assignment { get; set; }
        public Tag Tag { get; set; }
    }
}
