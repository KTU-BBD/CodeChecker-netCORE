using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeChecker.Data.Migrations
{
    public partial class PluralityAndAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Contests_ContestId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AspNetUsers_CreatorId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreator_Contests_ContestId",
                table: "ContestCreator");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreator_AspNetUsers_UserId",
                table: "ContestCreator");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipant_Contests_ContestId",
                table: "ContestParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipant_AspNetUsers_UserId",
                table: "ContestParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Input_Assignment_AssignmentId",
                table: "Input");

            migrationBuilder.DropForeignKey(
                name: "FK_Output_Input_InputId",
                table: "Output");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Contests_ContestId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_AspNetUsers_UserId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Assignment_AssignmentId",
                table: "TaskTag");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Tag_TagId",
                table: "TaskTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTag",
                table: "TaskTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Output",
                table: "Output");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Input",
                table: "Input");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestParticipant",
                table: "ContestParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestCreator",
                table: "ContestCreator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "TaskTag",
                newName: "TaskTags");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Output",
                newName: "Outputs");

            migrationBuilder.RenameTable(
                name: "Input",
                newName: "Inputs");

            migrationBuilder.RenameTable(
                name: "ContestParticipant",
                newName: "ContestParticipants");

            migrationBuilder.RenameTable(
                name: "ContestCreator",
                newName: "ContestCreators");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTag_TagId",
                table: "TaskTags",
                newName: "IX_TaskTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTag_AssignmentId",
                table: "TaskTags",
                newName: "IX_TaskTags_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_UserId",
                table: "Submissions",
                newName: "IX_Submissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_ContestId",
                table: "Submissions",
                newName: "IX_Submissions_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submissions",
                newName: "IX_Submissions_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Output_InputId",
                table: "Outputs",
                newName: "IX_Outputs_InputId");

            migrationBuilder.RenameIndex(
                name: "IX_Input_AssignmentId",
                table: "Inputs",
                newName: "IX_Inputs_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestParticipant_UserId",
                table: "ContestParticipants",
                newName: "IX_ContestParticipants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestParticipant_ContestId",
                table: "ContestParticipants",
                newName: "IX_ContestParticipants_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCreator_UserId",
                table: "ContestCreators",
                newName: "IX_ContestCreators_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCreator_ContestId",
                table: "ContestCreators",
                newName: "IX_ContestCreators_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CreatorId",
                table: "Assignments",
                newName: "IX_Assignments_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_ContestId",
                table: "Assignments",
                newName: "IX_Assignments_ContestId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Outputs",
                table: "Outputs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inputs",
                table: "Inputs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    Mimetype = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Assets_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Contests_ContestId",
                table: "Assignments",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_CreatorId",
                table: "Assignments",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Inputs_Assignments_AssignmentId",
                table: "Inputs",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Outputs_Inputs_InputId",
                table: "Outputs",
                column: "InputId",
                principalTable: "Inputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Contests_ContestId",
                table: "Submissions",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Assignments_AssignmentId",
                table: "TaskTags",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTags_Tags_TagId",
                table: "TaskTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Assets_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Contests_ContestId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_CreatorId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_Contests_ContestId",
                table: "ContestCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCreators_AspNetUsers_UserId",
                table: "ContestCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_Contests_ContestId",
                table: "ContestParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestParticipants_AspNetUsers_UserId",
                table: "ContestParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Inputs_Assignments_AssignmentId",
                table: "Inputs");

            migrationBuilder.DropForeignKey(
                name: "FK_Outputs_Inputs_InputId",
                table: "Outputs");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Contests_ContestId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Assignments_AssignmentId",
                table: "TaskTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTags_Tags_TagId",
                table: "TaskTags");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTags",
                table: "TaskTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Outputs",
                table: "Outputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inputs",
                table: "Inputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestParticipants",
                table: "ContestParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestCreators",
                table: "ContestCreators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TaskTags",
                newName: "TaskTag");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "Outputs",
                newName: "Output");

            migrationBuilder.RenameTable(
                name: "Inputs",
                newName: "Input");

            migrationBuilder.RenameTable(
                name: "ContestParticipants",
                newName: "ContestParticipant");

            migrationBuilder.RenameTable(
                name: "ContestCreators",
                newName: "ContestCreator");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTags_TagId",
                table: "TaskTag",
                newName: "IX_TaskTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTags_AssignmentId",
                table: "TaskTag",
                newName: "IX_TaskTag_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_UserId",
                table: "Submission",
                newName: "IX_Submission_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ContestId",
                table: "Submission",
                newName: "IX_Submission_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submission",
                newName: "IX_Submission_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Outputs_InputId",
                table: "Output",
                newName: "IX_Output_InputId");

            migrationBuilder.RenameIndex(
                name: "IX_Inputs_AssignmentId",
                table: "Input",
                newName: "IX_Input_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestParticipants_UserId",
                table: "ContestParticipant",
                newName: "IX_ContestParticipant_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestParticipants_ContestId",
                table: "ContestParticipant",
                newName: "IX_ContestParticipant_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCreators_UserId",
                table: "ContestCreator",
                newName: "IX_ContestCreator_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCreators_ContestId",
                table: "ContestCreator",
                newName: "IX_ContestCreator_ContestId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CreatorId",
                table: "Assignment",
                newName: "IX_Assignment_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_ContestId",
                table: "Assignment",
                newName: "IX_Assignment_ContestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTag",
                table: "TaskTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Output",
                table: "Output",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Input",
                table: "Input",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestParticipant",
                table: "ContestParticipant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestCreator",
                table: "ContestCreator",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Contests_ContestId",
                table: "Assignment",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AspNetUsers_CreatorId",
                table: "Assignment",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreator_Contests_ContestId",
                table: "ContestCreator",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCreator_AspNetUsers_UserId",
                table: "ContestCreator",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipant_Contests_ContestId",
                table: "ContestParticipant",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestParticipant_AspNetUsers_UserId",
                table: "ContestParticipant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Input_Assignment_AssignmentId",
                table: "Input",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Output_Input_InputId",
                table: "Output",
                column: "InputId",
                principalTable: "Input",
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
                name: "FK_Submission_Contests_ContestId",
                table: "Submission",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_AspNetUsers_UserId",
                table: "Submission",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Assignment_AssignmentId",
                table: "TaskTag",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Tag_TagId",
                table: "TaskTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
