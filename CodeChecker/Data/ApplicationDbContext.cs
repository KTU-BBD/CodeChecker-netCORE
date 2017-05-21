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
        public DbSet<ContestParticipant> ContestParticipants { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentTag> AssignmentTags { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<SubmissionGroup> SubmissionGroups { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<UserStatistic> UserStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contest>()
                .HasIndex(x => x.Name).IsUnique();


            modelBuilder.Entity<Input>()
                .HasOne(p => p.Output)
                .WithOne(i => i.Input)
                .HasForeignKey<Output>(b => b.InputId);

            //Contest participants
            modelBuilder.Entity<ContestParticipant>()
                .HasKey(x => new {x.ContestId, x.UserId});

            modelBuilder.Entity<ContestParticipant>()
                .HasOne(cc => cc.Contest)
                .WithMany(c => c.ContestParticipants)
                .HasForeignKey(cc => cc.ContestId);

            modelBuilder.Entity<ContestParticipant>()
                .HasOne(cc => cc.User)
                .WithMany(c => c.ContestParticipants)
                .HasForeignKey(cc => cc.UserId);


            //Assignment tags
            modelBuilder.Entity<AssignmentTag>()
                .HasKey(x => new {x.AssignmentId, x.TagId});

            modelBuilder.Entity<AssignmentTag>()
                .HasOne(cc => cc.Assignment)
                .WithMany(c => c.AssignmentTags)
                .HasForeignKey(cc => cc.AssignmentId);

            modelBuilder.Entity<AssignmentTag>()
                .HasOne(cc => cc.Tag)
                .WithMany(c => c.AssignmentTags)
                .HasForeignKey(cc => cc.TagId);



        }
    }
}