using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeReview2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ObjectOfVisitId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ObjectOfVisitId",
                table: "Review");

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_object",
                table: "Review",
                column: "id_object");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_object_of_visit_id_object",
                table: "Review",
                column: "id_object",
                principalTable: "object_of_visit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_object_of_visit_id_object",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_id_object",
                table: "Review");

            migrationBuilder.AddColumn<long>(
                name: "ObjectOfVisitId",
                table: "Review",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ObjectOfVisitId",
                table: "Review",
                column: "ObjectOfVisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review",
                column: "ObjectOfVisitId",
                principalTable: "object_of_visit",
                principalColumn: "Id");
        }
    }
}
