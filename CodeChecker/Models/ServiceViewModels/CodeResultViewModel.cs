namespace CodeChecker.Models.ServiceViewModels
{
    public class CodeResultViewModel
    {
        public double TimeSpent { get; set; }
        public string Output { get; set; }
        public string Verdict { get; set; }
        public string Language { get; set; }
    }
}