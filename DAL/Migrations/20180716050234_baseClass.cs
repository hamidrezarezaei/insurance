using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class baseClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "user_paymentType");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "user_paymentType");

            migrationBuilder.DropColumn(
                name: "active",
                table: "role_adminMenu");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "role_adminMenu");

            migrationBuilder.DropColumn(
                name: "active",
                table: "post_postCategory");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "post_postCategory");

            migrationBuilder.DropColumn(
                name: "active",
                table: "dataValue_category");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "dataValue_category");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "user_paymentType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "user_paymentType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "role_adminMenu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "role_adminMenu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "post_postCategory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "post_postCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "dataValue_category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "dataValue_category",
                nullable: false,
                defaultValue: 0);
        }
    }
}
