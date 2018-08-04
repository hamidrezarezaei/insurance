using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class add_hook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hooks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    updateUserId = table.Column<int>(nullable: false),
                    updateDateTime = table.Column<DateTime>(nullable: false),
                    siteId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    orderIndex = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hooks", x => x.id);
                    table.ForeignKey(
                        name: "FK_hooks_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sms_emails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    updateUserId = table.Column<int>(nullable: false),
                    updateDateTime = table.Column<DateTime>(nullable: false),
                    siteId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    orderIndex = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    text = table.Column<string>(nullable: true),
                    mobile = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sms_emails_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hooks_siteId",
                table: "hooks",
                column: "siteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sms_emails_siteId",
                table: "Sms_emails",
                column: "siteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hooks");

            migrationBuilder.DropTable(
                name: "Sms_emails");
        }
    }
}
