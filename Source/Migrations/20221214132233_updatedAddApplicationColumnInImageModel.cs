using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class updatedAddApplicationColumnInImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id_application",
                table: "image_object",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_image_object_id_application",
                table: "image_object",
                column: "id_application");

            migrationBuilder.AddForeignKey(
                name: "FK_image_object_Application_id_application",
                table: "image_object",
                column: "id_application",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_object_Application_id_application",
                table: "image_object");

            migrationBuilder.DropIndex(
                name: "IX_image_object_id_application",
                table: "image_object");

            migrationBuilder.DropColumn(
                name: "id_application",
                table: "image_object");
        }
    }
}
