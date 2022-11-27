using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeImageObjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object");

            migrationBuilder.AlterColumn<long>(
                name: "id_object",
                table: "image_object",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object",
                column: "id_object",
                principalTable: "object_of_visit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object");

            migrationBuilder.AlterColumn<long>(
                name: "id_object",
                table: "image_object",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_image_object_object_of_visit_id_object",
                table: "image_object",
                column: "id_object",
                principalTable: "object_of_visit",
                principalColumn: "Id");
        }
    }
}
