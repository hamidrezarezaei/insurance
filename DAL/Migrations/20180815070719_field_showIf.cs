using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class field_showIf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "showIf",
                table: "fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "showIf",
                table: "fields");
        }
    }
}
