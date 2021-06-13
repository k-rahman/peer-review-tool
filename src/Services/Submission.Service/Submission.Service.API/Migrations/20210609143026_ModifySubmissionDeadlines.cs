using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Submission.Service.API.Migrations
{
    public partial class ModifySubmissionDeadlines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_submissions_submissions_deadlines_submissions_deadline_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "submissions_deadlines");

            migrationBuilder.RenameColumn(
                name: "submissions_deadline_id",
                table: "submissions",
                newName: "submission_deadlines_id");

            migrationBuilder.RenameIndex(
                name: "IX_submissions_submissions_deadline_id",
                table: "submissions",
                newName: "IX_submissions_submission_deadlines_id");

            migrationBuilder.AlterColumn<string>(
                name: "author_id",
                table: "submissions",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "submission_deadlines",
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
                    table.PrimaryKey("PK_submission_deadlines", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_submissions_submission_deadlines_submission_deadlines_id",
                table: "submissions",
                column: "submission_deadlines_id",
                principalTable: "submission_deadlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_submissions_submission_deadlines_submission_deadlines_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "submission_deadlines");

            migrationBuilder.RenameColumn(
                name: "submission_deadlines_id",
                table: "submissions",
                newName: "submissions_deadline_id");

            migrationBuilder.RenameIndex(
                name: "IX_submissions_submission_deadlines_id",
                table: "submissions",
                newName: "IX_submissions_submissions_deadline_id");

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                table: "submissions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "submissions_deadlines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    submission_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    submission_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    uid = table.Column<Guid>(type: "uuid", nullable: false)
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
    }
}
