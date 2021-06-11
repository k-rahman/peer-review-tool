using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class RenameLinkToUidInTasksTable : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "link",
                            table: "tasks",
                            newName: "uid");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "uid",
                            table: "tasks",
                            newName: "link");
                }
        }
}
