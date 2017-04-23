using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Migrations
{
    public partial class ContestParticipantsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_Contests_ContestId",
                table: "ContestParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_AspNetUsers_UserId",
                table: "ContestParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants");

            migrationBuilder.DropIndex(
                name: "IX_ContestParticipants_ContestId",
                table: "ContestParticipants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContestParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestParticipants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ContestId",
                table: "ContestParticipants",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants",
                columns: new[] { "ContestId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipants_Contests_ContestId",
                table: "ContestParticipants",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipants_AspNetUsers_UserId",
                table: "ContestParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_Contests_ContestId",
                table: "ContestParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_AspNetUsers_UserId",
                table: "ContestParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContestParticipants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "ContestId",
                table: "ContestParticipants",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContestParticipants",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestParticipants_ContestId",
                table: "ContestParticipants",
                column: "ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipants_Contests_ContestId",
                table: "ContestParticipants",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipants_AspNetUsers_UserId",
                table: "ContestParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
