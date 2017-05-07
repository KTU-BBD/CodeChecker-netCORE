using System.Collections.Generic;

namespace CodeChecker.Models.ServiceViewModels
{
    public class DataFilterViewModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public Dictionary<string, string> Filter { get; set; }
        public Dictionary<string, string> Sorting { get; set; }

        public DataFilterViewModel()
        {
            Count = 10;
            Page = 1;
            Filter = new Dictionary<string, string>();
            Sorting = new Dictionary<string, string>();
        }
    }
}