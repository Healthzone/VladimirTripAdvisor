using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeForeignKeyObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_object_of_visit_id_owner",
                table: "object_of_visit",
                column: "id_owner");

            migrationBuilder.AddForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit",
                column: "id_owner",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit");

            migrationBuilder.DropIndex(
                name: "IX_object_of_visit_id_owner",
                table: "object_of_visit");
        }
    }
}
