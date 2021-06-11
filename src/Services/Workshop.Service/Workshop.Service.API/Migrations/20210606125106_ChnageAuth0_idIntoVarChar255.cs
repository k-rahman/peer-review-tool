using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class ChnageAuth0_idIntoVarChar255 : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<string>(
                            name: "auth0_id",
                            table: "participants",
                            type: "character varying(255)",
                            maxLength: 255,
                            nullable: false,
                            oldClrType: typeof(string),
                            oldType: "text");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<string>(
                            name: "auth0_id",
                            table: "participants",
                            type: "text",
                            nullable: false,
                            oldClrType: typeof(string),
                            oldType: "character varying(255)",
                            oldMaxLength: 255);
                }
        }
}
