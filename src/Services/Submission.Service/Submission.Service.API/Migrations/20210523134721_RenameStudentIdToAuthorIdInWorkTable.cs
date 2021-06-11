using Microsoft.EntityFrameworkCore.Migrations;

namespace Submission.Service.API.Migrations
{
        public partial class RenameStudentIdToAuthorIdInWorkTable : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "student_id",
                            table: "works",
                            newName: "author_id");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "author_id",
                            table: "works",
                            newName: "student_id");
                }
        }
}
