using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.ContestViewModels
{
    public class ChangeContestStatusWMessage
    {
        public ContestStatus Status { get; set; }
        public string Message { get; set; }
    }
}
