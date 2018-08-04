using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class dataType_father : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fatherid",
                table: "dataTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_dataTypes_fatherid",
                table: "dataTypes",
                column: "fatherid");

            migrationBuilder.AddForeignKey(
                name: "FK_dataTypes_dataTypes_fatherid",
                table: "dataTypes",
                column: "fatherid",
                principalTable: "dataTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dataTypes_dataTypes_fatherid",
                table: "dataTypes");

            migrationBuilder.DropIndex(
                name: "IX_dataTypes_fatherid",
                table: "dataTypes");

            migrationBuilder.DropColumn(
                name: "fatherid",
                table: "dataTypes");
        }
    }
}
