using CodeChecker.Models.UserViewModels;

namespace CodeChecker.Models.ContestViewModels
{
    public class ContestCreatorViewModel
    {
        public ContestViewModel Contest { get; set; }
        public UserViewModel User { get; set; }
    }
}