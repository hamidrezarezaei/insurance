using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class field_isHideLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHideLabel",
                table: "fields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHideLabel",
                table: "fields");
        }
    }
}
