using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class field_fourmula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "formula",
                table: "fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "formula",
                table: "fields");
        }
    }
}
