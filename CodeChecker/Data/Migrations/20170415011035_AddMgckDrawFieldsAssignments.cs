using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Data.Migrations
{
    public partial class AddMgckDrawFieldsAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Input_Task_TaskId",
                table: "Input");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Task_TaskId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Task_TaskId",
                table: "TaskTag");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "TaskTag",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTag_TaskId",
                table: "TaskTag",
                newName: "IX_TaskTag_AssignmentId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Submission",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_TaskId",
                table: "Submission",
                newName: "IX_Submission_AssignmentId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Input",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Input_TaskId",
                table: "Input",
                newName: "IX_Input_AssignmentId");

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContestId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InputType = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    MemoryLimit = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OutputType = table.Column<string>(nullable: true),
                    SolvedCount = table.Column<int>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_ContestId",
                table: "Assignment",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CreatorId",
                table: "Assignment",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Input_Assignment_AssignmentId",
                table: "Input",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Assignment_AssignmentId",
                table: "TaskTag",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Input_Assignment_AssignmentId",
                table: "Input");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Assignment_AssignmentId",
                table: "TaskTag");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "TaskTag",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTag_AssignmentId",
                table: "TaskTag",
                newName: "IX_TaskTag_TaskId");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Submission",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submission",
                newName: "IX_Submission_TaskId");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Input",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Input_AssignmentId",
                table: "Input",
                newName: "IX_Input_TaskId");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContestId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InputType = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    MemoryLimit = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OutputType = table.Column<string>(nullable: true),
                    SolvedCount = table.Column<int>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_ContestId",
                table: "Task",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatorId",
                table: "Task",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Input_Task_TaskId",
                table: "Input",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Task_TaskId",
                table: "Submission",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Task_TaskId",
                table: "TaskTag",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
