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
         public DbSet<ContestCreator> ContestCreators { get; set; }
         public DbSet<ContestParticipant> ContestParticipants { get; set; }
         public DbSet<Input> Inputs { get; set; }
         public DbSet<Output> Outputs { get; set; }
         public DbSet<Submission> Submissions { get; set; }
         public DbSet<Tag> Tags { get; set; }
         public DbSet<Assignment> Assignments { get; set; }
         public DbSet<TaskTag> TaskTags { get; set; }
         public DbSet<Asset> Assets { get; set; }
    }
}
