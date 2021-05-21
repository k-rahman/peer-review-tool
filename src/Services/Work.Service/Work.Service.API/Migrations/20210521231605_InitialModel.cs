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
                name: "WorksDeadlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmissionStart = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SubmissionEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksDeadlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Submitted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    WorksDeadlineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_WorksDeadlines_WorksDeadlineId",
                        column: x => x.WorksDeadlineId,
                        principalTable: "WorksDeadlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorksDeadlineId",
                table: "Works",
                column: "WorksDeadlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "WorksDeadlines");
        }
    }
}
