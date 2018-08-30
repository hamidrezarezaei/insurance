using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class post_meta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "metaDescription",
                table: "posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "metaKeywords",
                table: "posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metaDescription",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "metaKeywords",
                table: "posts");
        }
    }
}
