using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChecker.Models.Models
{
    public class Asset : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Mimetype { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}