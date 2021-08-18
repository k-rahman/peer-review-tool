using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Submission.Service.API.Migrations
{
    public partial class AddWorkshopUidColumnToSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_submissions_submission_deadlines_submission_deadlines_id",
                table: "submissions");

            migrationBuilder.DropIndex(
                name: "IX_submissions_submission_deadlines_id",
                table: "submissions");

            migrationBuilder.DropColumn(
                name: "submission_deadlines_id",
                table: "submissions");

            migrationBuilder.AddColumn<Guid>(
                name: "workshop_uid",
                table: "submissions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "workshop_uid",
                table: "submissions");

            migrationBuilder.AddColumn<int>(
                name: "submission_deadlines_id",
                table: "submissions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_submissions_submission_deadlines_id",
                table: "submissions",
                column: "submission_deadlines_id");

            migrationBuilder.AddForeignKey(
                name: "FK_submissions_submission_deadlines_submission_deadlines_id",
                table: "submissions",
                column: "submission_deadlines_id",
                principalTable: "submission_deadlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
