using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
    public partial class AddNameColumnToParticipantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "participants",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "participants");
        }
    }
}
