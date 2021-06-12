using Microsoft.EntityFrameworkCore.Migrations;

namespace Review.Service.API.Migrations
{
    public partial class RenameWorkIdColumnToSumissionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_submissions_work_id",
                table: "reviews");

            migrationBuilder.RenameColumn(
                name: "work_id",
                table: "reviews",
                newName: "submission_id");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_work_id",
                table: "reviews",
                newName: "IX_reviews_submission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_submissions_submission_id",
                table: "reviews",
                column: "submission_id",
                principalTable: "submissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_submissions_submission_id",
                table: "reviews");

            migrationBuilder.RenameColumn(
                name: "submission_id",
                table: "reviews",
                newName: "work_id");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_submission_id",
                table: "reviews",
                newName: "IX_reviews_work_id");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_submissions_work_id",
                table: "reviews",
                column: "work_id",
                principalTable: "submissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
