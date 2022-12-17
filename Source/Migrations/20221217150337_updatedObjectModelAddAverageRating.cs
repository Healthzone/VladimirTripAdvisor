using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class updatedObjectModelAddAverageRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "average_rating",
                table: "object_of_visit",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average_rating",
                table: "object_of_visit");
        }
    }
}
