namespace CodeChecker.Models.Models
{
    public class AssignmentTag
    {
        public int Id { get; set; } // Many-to-many relationship, I think ID is not needed
        public Assignment Assignment { get; set; }
        public Tag Tag { get; set; }
    }
}
