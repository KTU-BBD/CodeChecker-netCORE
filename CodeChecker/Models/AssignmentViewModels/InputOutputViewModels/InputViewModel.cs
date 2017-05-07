using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChecker.Models.AssignmentViewModels.InputOutputViewModels
{
    public class InputViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public OutputViewModel Output { get; set; }

    }
}
