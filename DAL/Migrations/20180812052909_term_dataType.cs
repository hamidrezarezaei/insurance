using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class term_dataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "dataTypeId",
                table: "terms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_terms_dataTypeId",
                table: "terms",
                column: "dataTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_terms_dataTypes_dataTypeId",
                table: "terms",
                column: "dataTypeId",
                principalTable: "dataTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_terms_dataTypes_dataTypeId",
                table: "terms");

            migrationBuilder.DropIndex(
                name: "IX_terms_dataTypeId",
                table: "terms");

            migrationBuilder.DropColumn(
                name: "dataTypeId",
                table: "terms");

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
    }
}
