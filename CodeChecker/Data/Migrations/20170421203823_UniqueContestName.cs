using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChecker.Migrations
{
    public partial class UniqueContestName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contests_Name",
                table: "Contests",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contests_Name",
                table: "Contests");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
