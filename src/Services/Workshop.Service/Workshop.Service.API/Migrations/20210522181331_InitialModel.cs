using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Workshop.Service.API.Migrations
{
        public partial class InitialModel : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.CreateTable(
                            name: "tasks",
                            columns: table => new
                            {
                                    id = table.Column<int>(type: "integer", nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                                    description = table.Column<string>(type: "text", nullable: false),
                                    link = table.Column<Guid>(type: "uuid", nullable: false),
                                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    review_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    review_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    published = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                                    InstructorId = table.Column<int>(type: "integer", nullable: false)
                            },
                            constraints: table =>
                            {
                                    table.PrimaryKey("PK_tasks", x => x.id);
                            });

                        migrationBuilder.CreateTable(
                            name: "criteria",
                            columns: table => new
                            {
                                    id = table.Column<int>(type: "integer", nullable: false)
                                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                    description = table.Column<string>(type: "text", nullable: false),
                                    max_points = table.Column<int>(type: "integer", nullable: false),
                                    task_id = table.Column<int>(type: "integer", nullable: false)
                            },
                            constraints: table =>
                            {
                                    table.PrimaryKey("PK_criteria", x => x.id);
                                    table.ForeignKey(
                            name: "FK_criteria_tasks_task_id",
                            column: x => x.task_id,
                            principalTable: "tasks",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Cascade);
                            });

                        migrationBuilder.CreateIndex(
                            name: "IX_criteria_task_id",
                            table: "criteria",
                            column: "task_id");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.DropTable(
                            name: "criteria");

                        migrationBuilder.DropTable(
                            name: "tasks");
                }
        }
}
