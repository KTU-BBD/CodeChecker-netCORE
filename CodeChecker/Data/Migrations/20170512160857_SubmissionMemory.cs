using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class SubmissionMemory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Memory",
                table: "SubmissionGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Memory",
                table: "Submissions",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Memory",
                table: "SubmissionGroups");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "Submissions");
        }
    }
}
