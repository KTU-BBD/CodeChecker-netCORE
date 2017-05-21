using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class SubmissionGroupColumnMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SubmissionGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SubmissionGroups");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Submissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Submissions",
                nullable: true);
        }
    }
}
