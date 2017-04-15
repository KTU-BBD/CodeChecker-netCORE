using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class Output
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Input Input { get; set; }
    }
}
