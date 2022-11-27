using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class changeReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Review",
                newName: "score");

            migrationBuilder.AlterColumn<long>(
                name: "ObjectOfVisitId",
                table: "Review",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review",
                column: "ObjectOfVisitId",
                principalTable: "object_of_visit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Review",
                newName: "Score");

            migrationBuilder.AlterColumn<long>(
                name: "ObjectOfVisitId",
                table: "Review",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_object_of_visit_ObjectOfVisitId",
                table: "Review",
                column: "ObjectOfVisitId",
                principalTable: "object_of_visit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
