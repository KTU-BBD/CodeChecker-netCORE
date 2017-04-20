using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Migrations
{
    public partial class ContestCreatorRelatioship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_Contests_ContestId",
                table: "ContestCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_AspNetUsers_UserId",
                table: "ContestCreators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators");

            migrationBuilder.DropIndex(
                name: "IX_ContestCreators_ContestId",
                table: "ContestCreators");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContestCreators");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Contests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestCreators",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ContestId",
                table: "ContestCreators",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Contests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Contests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators",
                columns: new[] { "ContestId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreators_Contests_ContestId",
                table: "ContestCreators",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreators_AspNetUsers_UserId",
                table: "ContestCreators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_Contests_ContestId",
                table: "ContestCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_AspNetUsers_UserId",
                table: "ContestCreators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Contests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestCreators",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "ContestId",
                table: "ContestCreators",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContestCreators",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Contests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestCreators_ContestId",
                table: "ContestCreators",
                column: "ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreators_Contests_ContestId",
                table: "ContestCreators",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreators_AspNetUsers_UserId",
                table: "ContestCreators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
