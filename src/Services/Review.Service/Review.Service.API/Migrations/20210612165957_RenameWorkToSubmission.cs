using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Review.Service.API.Migrations
{
    public partial class RenameWorkToSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_works_work_id",
                table: "reviews");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    author_id = table.Column<string>(type: "text", nullable: true),
                    workshop_uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submissions", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_submissions_work_id",
                table: "reviews",
                column: "work_id",
                principalTable: "submissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_submissions_work_id",
                table: "reviews");

            migrationBuilder.DropTable(
                name: "submissions");

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    task_uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_works_work_id",
                table: "reviews",
                column: "work_id",
                principalTable: "works",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
