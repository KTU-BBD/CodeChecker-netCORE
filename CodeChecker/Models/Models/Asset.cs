using System;

namespace CodeChecker.Models.Models
{
    public class Asset : SoftDeletable
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Mimetype { get; set; }
    }
}