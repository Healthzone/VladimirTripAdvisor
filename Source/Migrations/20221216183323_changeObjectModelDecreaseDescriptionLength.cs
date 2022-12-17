using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeObjectModelDecreaseDescriptionLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "place_description",
                table: "object_of_visit",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2500)",
                oldMaxLength: 2500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "place_description",
                table: "object_of_visit",
                type: "varchar(2500)",
                maxLength: 2500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
