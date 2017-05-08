using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Migrations
{
    public partial class TagRelationshipWithAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTags_Assignments_AssignmentId",
                table: "AssignmentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTags_Tags_TagId",
                table: "AssignmentTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTags",
                table: "AssignmentTags");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentTags_AssignmentId",
                table: "AssignmentTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssignmentTags");

            migrationBuilder.AlterColumn<long>(
                name: "TagId",
                table: "AssignmentTags",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AssignmentId",
                table: "AssignmentTags",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTags",
                table: "AssignmentTags",
                columns: new[] { "AssignmentId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTags_Assignments_AssignmentId",
                table: "AssignmentTags",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTags_Tags_TagId",
                table: "AssignmentTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTags_Assignments_AssignmentId",
                table: "AssignmentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTags_Tags_TagId",
                table: "AssignmentTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTags",
                table: "AssignmentTags");

            migrationBuilder.AlterColumn<long>(
                name: "TagId",
                table: "AssignmentTags",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "AssignmentId",
                table: "AssignmentTags",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AssignmentTags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTags",
                table: "AssignmentTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTags_AssignmentId",
                table: "AssignmentTags",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTags_Assignments_AssignmentId",
                table: "AssignmentTags",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTags_Tags_TagId",
                table: "AssignmentTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
