using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeChecker.Models;
using CodeChecker.Models.Models;

namespace CodeChecker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         public DbSet<Contest> Contests { get; set; }
         public DbSet<ContestCreator> ContestCreator { get; set; }
         public DbSet<ContestParticipant> ContestParticipant { get; set; }
         public DbSet<Input> Input { get; set; }
         public DbSet<Output> Output { get; set; }
         public DbSet<Submission> Submission { get; set; }
         public DbSet<Tag> Tag { get; set; }
         public DbSet<Assignment> Assignment { get; set; }
         public DbSet<TaskTag> TaskTag { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
