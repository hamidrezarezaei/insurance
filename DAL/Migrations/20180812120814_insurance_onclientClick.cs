using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class insurance_onclientClick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "onClientClick",
                table: "insurances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "onClientClick",
                table: "insurances");
        }
    }
}
