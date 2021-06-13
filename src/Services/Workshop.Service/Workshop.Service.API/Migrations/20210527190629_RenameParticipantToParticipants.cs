using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class RenameParticipantToParticipants : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.DropForeignKey(
                            name: "FK_task_participants_participant_participant_id",
                            table: "task_participants");

                        migrationBuilder.DropPrimaryKey(
                            name: "PK_participant",
                            table: "participant");

                        migrationBuilder.RenameTable(
                            name: "participant",
                            newName: "participants");

                        migrationBuilder.AddPrimaryKey(
                            name: "PK_participants",
                            table: "participants",
                            column: "id");

                        migrationBuilder.AddForeignKey(
                            name: "FK_task_participants_participants_participant_id",
                            table: "task_participants",
                            column: "participant_id",
                            principalTable: "participants",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Cascade);
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.DropForeignKey(
                            name: "FK_task_participants_participants_participant_id",
                            table: "task_participants");

                        migrationBuilder.DropPrimaryKey(
                            name: "PK_participants",
                            table: "participants");

                        migrationBuilder.RenameTable(
                            name: "participants",
                            newName: "participant");

                        migrationBuilder.AddPrimaryKey(
                            name: "PK_participant",
                            table: "participant",
                            column: "id");

                        migrationBuilder.AddForeignKey(
                            name: "FK_task_participants_participant_participant_id",
                            table: "task_participants",
                            column: "participant_id",
                            principalTable: "participant",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Cascade);
                }
        }
}
