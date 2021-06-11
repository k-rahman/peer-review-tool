using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Workshop.Service.API.Migrations
{
        public partial class AddParticipantsAndTaskParticipantsTables : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.RenameColumn(
                            name: "InstructorId",
                            table: "tasks",
                            newName: "instructor_id");

                        migrationBuilder.CreateTable(
                            name: "participant",
                            columns: table => new
                            {
                                    id = table.Column<int>(type: "integer", nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                            },
                            constraints: table =>
                            {
                                    table.PrimaryKey("PK_participant", x => x.id);
                            });

                        migrationBuilder.CreateTable(
                            name: "task_participants",
                            columns: table => new
                            {
                                    task_id = table.Column<int>(type: "integer", nullable: false),
                                    participant_id = table.Column<int>(type: "integer", nullable: false)
                            },
                            constraints: table =>
                            {
                                    table.PrimaryKey("PK_task_participants", x => new { x.task_id, x.participant_id });
                                    table.ForeignKey(
                            name: "FK_task_participants_tasks_task_id",
                            column: x => x.task_id,
                            principalTable: "tasks",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Cascade);
                                    table.ForeignKey(
                            name: "FK_task_participants_participant_participant_id",
                            column: x => x.participant_id,
                            principalTable: "participant",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Cascade);
                            });

                        migrationBuilder.CreateIndex(
                            name: "IX_task_participants_task_id",
                            table: "task_participants",
                            column: "task_id");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.DropTable(
                            name: "task_participants");

                        migrationBuilder.DropTable(
                            name: "participant");

                        migrationBuilder.RenameColumn(
                            name: "instructor_id",
                            table: "tasks",
                            newName: "InstructorId");
                }
        }
}
