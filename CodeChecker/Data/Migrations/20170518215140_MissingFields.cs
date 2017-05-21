using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class MissingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContestId",
                table: "SubmissionGroups",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Points",
                table: "SubmissionGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionGroups_ContestId",
                table: "SubmissionGroups",
                column: "ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionGroups_Contests_ContestId",
                table: "SubmissionGroups",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionGroups_Contests_ContestId",
                table: "SubmissionGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionGroups_ContestId",
                table: "SubmissionGroups");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "SubmissionGroups");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "SubmissionGroups");
        }
    }
}
