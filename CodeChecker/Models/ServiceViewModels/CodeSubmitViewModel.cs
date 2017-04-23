namespace CodeChecker.Models.ServiceViewModels
{
    public class CodeSubmitViewModel
    {
        public int memoryLimit { get; set; }
        public string code { get; set; }
        public string inputText { get; set; }
        public int timeLimit { get; set; }
        public string language { get; set; }
    }
}