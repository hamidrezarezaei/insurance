using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class refactor_sms_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hookid",
                table: "Sms_emails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sms_emails_hookid",
                table: "Sms_emails",
                column: "hookid");

            migrationBuilder.AddForeignKey(
                name: "FK_Sms_emails_hooks_hookid",
                table: "Sms_emails",
                column: "hookid",
                principalTable: "hooks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sms_emails_hooks_hookid",
                table: "Sms_emails");

            migrationBuilder.DropIndex(
                name: "IX_Sms_emails_hookid",
                table: "Sms_emails");

            migrationBuilder.DropColumn(
                name: "hookid",
                table: "Sms_emails");
        }
    }
}
