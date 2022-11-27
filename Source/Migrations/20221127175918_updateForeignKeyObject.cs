using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class updateForeignKeyObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit");

            migrationBuilder.AlterColumn<long>(
                name: "id_owner",
                table: "object_of_visit",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit",
                column: "id_owner",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit");

            migrationBuilder.AlterColumn<long>(
                name: "id_owner",
                table: "object_of_visit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_object_of_visit_User_id_owner",
                table: "object_of_visit",
                column: "id_owner",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
