using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class category_dataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dataTypeId",
                table: "categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_dataTypeId",
                table: "categories",
                column: "dataTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_dataTypes_dataTypeId",
                table: "categories",
                column: "dataTypeId",
                principalTable: "dataTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_dataTypes_dataTypeId",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_dataTypeId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "dataTypeId",
                table: "categories");
        }
    }
}
