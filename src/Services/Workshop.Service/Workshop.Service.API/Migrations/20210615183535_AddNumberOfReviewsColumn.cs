using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
    public partial class AddNumberOfReviewsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "number_of_reviews",
                table: "workshops",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_of_reviews",
                table: "workshops");
        }
    }
}
