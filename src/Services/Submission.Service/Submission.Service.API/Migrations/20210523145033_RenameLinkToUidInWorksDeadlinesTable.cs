using Microsoft.EntityFrameworkCore.Migrations;

namespace Submission.Service.API.Migrations
{
        public partial class RenameLinkToUidInWorksDeadlinesTable : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "link",
                            table: "works_deadlines",
                            newName: "Uid");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "Uid",
                            table: "works_deadlines",
                            newName: "link");
                }
        }
}
