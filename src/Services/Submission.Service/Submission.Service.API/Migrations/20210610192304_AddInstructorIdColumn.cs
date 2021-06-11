using Microsoft.EntityFrameworkCore.Migrations;

namespace Submission.Service.API.Migrations
{
    public partial class AddInstructorIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "instructor_id",
                table: "submission_deadlines",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instructor_id",
                table: "submission_deadlines");
        }
    }
}
