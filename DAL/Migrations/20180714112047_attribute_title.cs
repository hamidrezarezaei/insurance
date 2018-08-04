using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class attribute_title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "attributes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "attributes");
        }
    }
}
