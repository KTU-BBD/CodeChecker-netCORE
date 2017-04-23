using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class ContestsRelRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestCreators");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Contests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contests_CreatorId",
                table: "Contests",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contests_AspNetUsers_CreatorId",
                table: "Contests",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contests_AspNetUsers_CreatorId",
                table: "Contests");

            migrationBuilder.DropIndex(
                name: "IX_Contests_CreatorId",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Contests");

            migrationBuilder.CreateTable(
                name: "ContestCreators",
                columns: table => new
                {
                    ContestId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestCreators", x => new { x.ContestId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContestCreators_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestCreators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestCreators_UserId",
                table: "ContestCreators",
                column: "UserId");
        }
    }
}
