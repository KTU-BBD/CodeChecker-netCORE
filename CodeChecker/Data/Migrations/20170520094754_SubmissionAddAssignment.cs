using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class SubmissionAddAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionGroups_Contests_ContestId",
                table: "SubmissionGroups");

            migrationBuilder.RenameColumn(
                name: "ContestId",
                table: "SubmissionGroups",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionGroups_ContestId",
                table: "SubmissionGroups",
                newName: "IX_SubmissionGroups_AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionGroups_Assignments_AssignmentId",
                table: "SubmissionGroups",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionGroups_Assignments_AssignmentId",
                table: "SubmissionGroups");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "SubmissionGroups",
                newName: "ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionGroups_AssignmentId",
                table: "SubmissionGroups",
                newName: "IX_SubmissionGroups_ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionGroups_Contests_ContestId",
                table: "SubmissionGroups",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
