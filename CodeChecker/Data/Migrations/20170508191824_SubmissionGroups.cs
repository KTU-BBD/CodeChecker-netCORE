using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Migrations
{
    public partial class SubmissionGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubmissionGroupId",
                table: "Submissions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SubmissionGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_SubmissionGroupId",
                table: "Submissions",
                column: "SubmissionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_SubmissionGroups_SubmissionGroupId",
                table: "Submissions",
                column: "SubmissionGroupId",
                principalTable: "SubmissionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_SubmissionGroups_SubmissionGroupId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "SubmissionGroups");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_SubmissionGroupId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "SubmissionGroupId",
                table: "Submissions");
        }
    }
}
