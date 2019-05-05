using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class tooltip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tooltip",
                table: "fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tooltip",
                table: "fields");
        }
    }
}
