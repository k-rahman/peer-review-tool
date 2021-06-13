using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class CreateUniqueIndexOnUidInTaskTable : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.CreateIndex(
                            name: "IX_tasks_uid",
                            table: "tasks",
                            column: "uid",
                            unique: true);
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.DropIndex(
                            name: "IX_tasks_uid",
                            table: "tasks");
                }
        }
}
