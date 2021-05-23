using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Work.Service.API.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "works_deadlines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    link = table.Column<Guid>(type: "uuid", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_deadlines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    submitted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    works_deadline_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.id);
                    table.ForeignKey(
                        name: "FK_works_works_deadlines_works_deadline_id",
                        column: x => x.works_deadline_id,
                        principalTable: "works_deadlines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_works_works_deadline_id",
                table: "works",
                column: "works_deadline_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "works_deadlines");
        }
    }
}
