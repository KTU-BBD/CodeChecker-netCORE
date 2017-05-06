namespace CodeChecker.Models.Models
{
    public class Output : BaseModel
    {
        public string Text { get; set; }

        public long InputId { get; set; }
        public Input Input { get; set; }
    }
}