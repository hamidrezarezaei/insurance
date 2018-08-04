using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class base_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "settings");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "settings");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "orders");

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
        }
    }
}
