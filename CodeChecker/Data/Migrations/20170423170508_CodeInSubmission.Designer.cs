using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CodeChecker.Data;
using CodeChecker.Models.Models.Enums;

namespace CodeChecker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170423170508_CodeInSubmission")]
    partial class CodeInSubmission
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodeChecker.Models.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<long?>("ProfileImageId");

                    b.Property<int>("Rating");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Asset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Mimetype");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalName");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Assignment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ContestId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatorId");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("InputType");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MaxPoints");

                    b.Property<int>("MemoryLimit");

                    b.Property<string>("Name");

                    b.Property<string>("OutputType");

                    b.Property<int>("SolvedCount");

                    b.Property<int>("TimeLimit");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.AssignmentTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssignmentId");

                    b.Property<long?>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("TagId");

                    b.ToTable("AssignmentTags");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Contest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatorId");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndAt");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<DateTime>("StartAt");

                    b.Property<int>("Status");

                    b.Property<int>("SuccessfulSubmit");

                    b.Property<int>("UnsuccessfulSubmit");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.ContestParticipant", b =>
                {
                    b.Property<long>("ContestId");

                    b.Property<string>("UserId");

                    b.HasKey("ContestId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ContestParticipants");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Input", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssignmentId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Text");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Inputs");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Output", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("InputId");

                    b.Property<string>("Text");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("InputId")
                        .IsUnique();

                    b.ToTable("Outputs");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Submission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssignmentId");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Language");

                    b.Property<int>("TimeMs");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserId");

                    b.Property<string>("Verdict");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.ApplicationUser", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Asset", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Assignment", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Contest", "Contest")
                        .WithMany("Assignments")
                        .HasForeignKey("ContestId");

                    b.HasOne("CodeChecker.Models.Models.ApplicationUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.AssignmentTag", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId");

                    b.HasOne("CodeChecker.Models.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Contest", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.ApplicationUser", "Creator")
                        .WithMany("Contests")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.ContestParticipant", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Contest", "Contest")
                        .WithMany("ContestParticipants")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodeChecker.Models.Models.ApplicationUser", "User")
                        .WithMany("ContestParticipants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Input", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Assignment", "Assignment")
                        .WithMany("Inputs")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Output", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Input", "Input")
                        .WithOne("Output")
                        .HasForeignKey("CodeChecker.Models.Models.Output", "InputId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CodeChecker.Models.Models.Submission", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId");

                    b.HasOne("CodeChecker.Models.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CodeChecker.Models.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodeChecker.Models.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
