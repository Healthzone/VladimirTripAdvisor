using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeImageColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_object_object_of_visit_ObjectOfVisitId",
                table: "image_object");

            migrationBuilder.DropIndex(
                name: "IX_image_object_ObjectOfVisitId",
                table: "image_object");

            migrationBuilder.DropColumn(
                name: "ObjectOfVisitId",
                table: "image_object");

            migrationBuilder.CreateIndex(
                name: "IX_image_object_id_object",
                table: "image_object",
                column: "id_object");

            migrationBuilder.AddForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object",
                column: "id_object",
                principalTable: "object_of_visit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object");

            migrationBuilder.DropIndex(
                name: "IX_image_object_id_object",
                table: "image_object");

            migrationBuilder.AddColumn<long>(
                name: "ObjectOfVisitId",
                table: "image_object",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_image_object_ObjectOfVisitId",
                table: "image_object",
                column: "ObjectOfVisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_image_object_object_of_visit_ObjectOfVisitId",
                table: "image_object",
                column: "ObjectOfVisitId",
                principalTable: "object_of_visit",
                principalColumn: "Id");
        }
    }
}
