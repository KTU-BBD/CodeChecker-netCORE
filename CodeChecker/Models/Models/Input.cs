using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.Models
{
    public class Input
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Assignment Assignment { get; set; }
    }
}
