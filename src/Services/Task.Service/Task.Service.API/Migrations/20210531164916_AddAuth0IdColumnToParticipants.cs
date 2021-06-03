using Microsoft.EntityFrameworkCore.Migrations;

namespace Task.Service.API.Migrations
{
    public partial class AddAuth0IdColumnToParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "auth0_id",
                table: "participants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "auth0_id",
                table: "participants");
        }
    }
}
