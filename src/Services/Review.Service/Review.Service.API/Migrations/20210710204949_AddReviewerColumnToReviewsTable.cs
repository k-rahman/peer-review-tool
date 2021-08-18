using Microsoft.EntityFrameworkCore.Migrations;

namespace Review.Service.API.Migrations
{
    public partial class AddReviewerColumnToReviewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reviewer",
                table: "reviews",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewer",
                table: "reviews");
        }
    }
}
