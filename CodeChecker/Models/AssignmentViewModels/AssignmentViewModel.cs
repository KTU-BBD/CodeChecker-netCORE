namespace CodeChecker.Models.AssignmentViewModels
{
    public class AssignmentViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MemoryLimit { get; set; }
        public int TimeLimit { get; set; }

        public string InputType { get; set; }
        public string OutputType { get; set; }
    }
}