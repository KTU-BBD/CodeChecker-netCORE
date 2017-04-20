﻿namespace CodeChecker.Models.Models
{
    public class ContestCreator
    {
        public int Id { get; set; } // Many-to-many relationship, I think ID is not needed
        public ApplicationUser User { get; set; }
        public Contest Contest { get; set; }
    }
}
