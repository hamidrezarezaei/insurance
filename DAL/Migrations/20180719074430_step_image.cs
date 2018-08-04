using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class step_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "navigationCssClass",
                table: "steps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "navigationCssClass",
                table: "steps");
        }
    }
}
