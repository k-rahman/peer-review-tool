using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class AlterInstructorIdDataTypeToString : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<string>(
                            name: "instructor_id",
                            table: "tasks",
                            type: "character varying(255)",
                            maxLength: 255,
                            nullable: false,
                            oldClrType: typeof(int),
                            oldType: "integer");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<int>(
                            name: "instructor_id",
                            table: "tasks",
                            type: "integer",
                            nullable: false,
                            oldClrType: typeof(string),
                            oldType: "character varying(255)",
                            oldMaxLength: 255);
                }
        }
}
