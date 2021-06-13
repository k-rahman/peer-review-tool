using Microsoft.EntityFrameworkCore.Migrations;

namespace Review.Service.API.Migrations
{
    public partial class RenameTaskToWorkshopInCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "task_uid",
                table: "criteria",
                newName: "workshop_uid");

            migrationBuilder.AlterColumn<string>(
                name: "reviewer_id",
                table: "reviews",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "workshop_uid",
                table: "criteria",
                newName: "task_uid");

            migrationBuilder.AlterColumn<int>(
                name: "reviewer_id",
                table: "reviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
