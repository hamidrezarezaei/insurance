using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class setting_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "settings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "settings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "settings");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "settings");

            migrationBuilder.DropColumn(
                name: "title",
                table: "settings");
        }
    }
}
