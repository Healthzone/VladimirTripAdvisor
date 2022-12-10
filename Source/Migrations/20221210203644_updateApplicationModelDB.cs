using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class updateApplicationModelDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "application_status",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "application_status",
                table: "Application");
        }
    }
}
