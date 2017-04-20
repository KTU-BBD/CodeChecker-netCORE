using System;

namespace CodeChecker.Models.Models
{
    public class SoftDeletable : BaseModel
    {
        public DateTime? DeletedAt { get; set; }

        public SoftDeletable()
        {
            DeletedAt = null;
        }

        public virtual void Delete()
        {
            DeletedAt = DateTime.Now;
        }

        public virtual void Recover()
        {
            DeletedAt = null;
        }
    }
}