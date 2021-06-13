using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Submission.Service.API.Migrations
{
    public partial class RenameWorksToSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_works_works_deadlines_works_deadline_id",
                table: "works");

            migrationBuilder.DropTable(
                name: "works_deadlines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_works",
                table: "works");

            migrationBuilder.RenameTable(
                name: "works",
                newName: "submissions");

            migrationBuilder.RenameColumn(
                name: "works_deadline_id",
                table: "submissions",
                newName: "submissions_deadline_id");

            migrationBuilder.RenameIndex(
                name: "IX_works_works_deadline_id",
                table: "submissions",
                newName: "IX_submissions_submissions_deadline_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_submissions",
                table: "submissions",
                column: "id");

            migrationBuilder.CreateTable(
                name: "submissions_deadlines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uid = table.Column<Guid>(type: "uuid", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submissions_deadlines", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_submissions_submissions_deadlines_submissions_deadline_id",
                table: "submissions",
                column: "submissions_deadline_id",
                principalTable: "submissions_deadlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_submissions_submissions_deadlines_submissions_deadline_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "submissions_deadlines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_submissions",
                table: "submissions");

            migrationBuilder.RenameTable(
                name: "submissions",
                newName: "works");

            migrationBuilder.RenameColumn(
                name: "submissions_deadline_id",
                table: "works",
                newName: "works_deadline_id");

            migrationBuilder.RenameIndex(
                name: "IX_submissions_submissions_deadline_id",
                table: "works",
                newName: "IX_works_works_deadline_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_works",
                table: "works",
                column: "id");

            migrationBuilder.CreateTable(
                name: "works_deadlines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_deadlines", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_works_works_deadlines_works_deadline_id",
                table: "works",
                column: "works_deadline_id",
                principalTable: "works_deadlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
