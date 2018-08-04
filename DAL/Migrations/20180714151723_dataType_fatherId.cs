using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class dataType_fatherId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dataTypes_dataTypes_fatherid",
                table: "dataTypes");

            migrationBuilder.RenameColumn(
                name: "fatherid",
                table: "dataTypes",
                newName: "fatherId");

            migrationBuilder.RenameIndex(
                name: "IX_dataTypes_fatherid",
                table: "dataTypes",
                newName: "IX_dataTypes_fatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_dataTypes_dataTypes_fatherId",
                table: "dataTypes",
                column: "fatherId",
                principalTable: "dataTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dataTypes_dataTypes_fatherId",
                table: "dataTypes");

            migrationBuilder.RenameColumn(
                name: "fatherId",
                table: "dataTypes",
                newName: "fatherid");

            migrationBuilder.RenameIndex(
                name: "IX_dataTypes_fatherId",
                table: "dataTypes",
                newName: "IX_dataTypes_fatherid");

            migrationBuilder.AddForeignKey(
                name: "FK_dataTypes_dataTypes_fatherid",
                table: "dataTypes",
                column: "fatherid",
                principalTable: "dataTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
