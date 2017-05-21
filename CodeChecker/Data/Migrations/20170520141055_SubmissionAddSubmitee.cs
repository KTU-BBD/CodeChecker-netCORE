using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class SubmissionAddSubmitee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmiteeId",
                table: "SubmissionGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionGroups_SubmiteeId",
                table: "SubmissionGroups",
                column: "SubmiteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionGroups_AspNetUsers_SubmiteeId",
                table: "SubmissionGroups",
                column: "SubmiteeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionGroups_AspNetUsers_SubmiteeId",
                table: "SubmissionGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionGroups_SubmiteeId",
                table: "SubmissionGroups");

            migrationBuilder.DropColumn(
                name: "SubmiteeId",
                table: "SubmissionGroups");
        }
    }
}
