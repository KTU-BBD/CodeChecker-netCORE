using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class SubmissionGroupLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Time",
                table: "SubmissionGroups",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "SubmissionGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "SubmissionGroups");

            migrationBuilder.AlterColumn<float>(
                name: "Time",
                table: "SubmissionGroups",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
