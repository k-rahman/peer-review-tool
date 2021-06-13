using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Workshop.Service.API.Migrations
{
    public partial class RenameTaskToWorkshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_criteria_tasks_task_id",
                table: "criteria");

            migrationBuilder.DropTable(
                name: "task_participants");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.RenameColumn(
                name: "task_id",
                table: "criteria",
                newName: "workshop_id");

            migrationBuilder.RenameIndex(
                name: "IX_criteria_task_id",
                table: "criteria",
                newName: "IX_criteria_workshop_id");

            migrationBuilder.CreateTable(
                name: "workshops",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    review_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    review_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    instructor_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshops", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workshop_participants",
                columns: table => new
                {
                    participant_id = table.Column<int>(type: "integer", nullable: false),
                    workshop_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshop_participants", x => new { x.participant_id, x.workshop_id });
                    table.ForeignKey(
                        name: "FK_workshop_participants_participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workshop_participants_workshops_workshop_id",
                        column: x => x.workshop_id,
                        principalTable: "workshops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_workshop_participants_workshop_id",
                table: "workshop_participants",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_workshops_uid",
                table: "workshops",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_criteria_workshops_workshop_id",
                table: "criteria",
                column: "workshop_id",
                principalTable: "workshops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_criteria_workshops_workshop_id",
                table: "criteria");

            migrationBuilder.DropTable(
                name: "workshop_participants");

            migrationBuilder.DropTable(
                name: "workshops");

            migrationBuilder.RenameColumn(
                name: "workshop_id",
                table: "criteria",
                newName: "task_id");

            migrationBuilder.RenameIndex(
                name: "IX_criteria_workshop_id",
                table: "criteria",
                newName: "IX_criteria_task_id");

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    instructor_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    review_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    review_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task_participants",
                columns: table => new
                {
                    participant_id = table.Column<int>(type: "integer", nullable: false),
                    task_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_participants", x => new { x.participant_id, x.task_id });
                    table.ForeignKey(
                        name: "FK_task_participants_participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_participants_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_participants_task_id",
                table: "task_participants",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_uid",
                table: "tasks",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_criteria_tasks_task_id",
                table: "criteria",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
