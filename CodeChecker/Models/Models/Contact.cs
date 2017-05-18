using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Models.Models
{
    public class Contact : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string ResponseMessage { get; set; }
        public ContactStatus Status { get; set; }
    }
}