using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    public partial class addImageToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "additional_address_info",
                table: "object_of_visit",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "image_object",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_object = table.Column<long>(type: "bigint", nullable: true),
                    ObjectOfVisitId = table.Column<long>(type: "bigint", nullable: true),
                    image_byte = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_object", x => x.Id);
                    table.ForeignKey(
                        name: "FK_image_object_object_of_visit_ObjectOfVisitId",
                        column: x => x.ObjectOfVisitId,
                        principalTable: "object_of_visit",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_image_object_ObjectOfVisitId",
                table: "image_object",
                column: "ObjectOfVisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image_object");

            migrationBuilder.UpdateData(
                table: "object_of_visit",
                keyColumn: "additional_address_info",
                keyValue: null,
                column: "additional_address_info",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "additional_address_info",
                table: "object_of_visit",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
