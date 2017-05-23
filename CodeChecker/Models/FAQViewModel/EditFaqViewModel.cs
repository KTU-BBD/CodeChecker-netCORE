using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.FAQViewModel
{
    public class EditFaqViewModel
    {
        public Int64 Id { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Required]
        public string Question { get; set; }
    }
}
