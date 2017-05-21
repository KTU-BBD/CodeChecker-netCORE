namespace CodeChecker.Models.EmailMessageModels
{
    public class CreateDeleteMessageViewModel
    {
        public string Reason { get; set; }
        public string ItemName { get; set; }
        public string CreatorName { get; set; }
        public long ItemId { get; set; }
    }
}