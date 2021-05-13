using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Task.Service.API.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Publish = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SubmissionStart = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SubmissionEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReviewStart = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReviewEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    InstructorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "InstructorId", "Link", "Name", "Publish", "ReviewEnd", "ReviewStart", "SubmissionEnd", "SubmissionStart" },
                values: new object[,]
                {
                    { 1, "first task description", 1, "first task link", "first task", new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 508, DateTimeKind.Unspecified).AddTicks(8101), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(3679), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(3677), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(3674), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(3660), new TimeSpan(0, 3, 0, 0, 0)) },
                    { 2, "second task description", 1, "second task link", "second task", new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(4523), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(4537), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(4536), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(4534), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 5, 14, 1, 12, 43, 511, DateTimeKind.Unspecified).AddTicks(4532), new TimeSpan(0, 3, 0, 0, 0)) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
