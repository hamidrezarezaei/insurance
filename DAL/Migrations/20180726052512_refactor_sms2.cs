using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class refactor_sms2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sms_emails");

            migrationBuilder.CreateTable(
                name: "smses",
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
                    hookid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smses", x => x.id);
                    table.ForeignKey(
                        name: "FK_smses_hooks_hookid",
                        column: x => x.hookid,
                        principalTable: "hooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_smses_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_smses_hookid",
                table: "smses",
                column: "hookid");

            migrationBuilder.CreateIndex(
                name: "IX_smses_siteId",
                table: "smses",
                column: "siteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "smses");

            migrationBuilder.CreateTable(
                name: "sms_emails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active = table.Column<bool>(nullable: false),
                    hookId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    mobile = table.Column<string>(nullable: true),
                    orderIndex = table.Column<int>(nullable: false),
                    siteId = table.Column<int>(nullable: false),
                    text = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    updateDateTime = table.Column<DateTime>(nullable: false),
                    updateUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sms_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_sms_emails_hooks_hookId",
                        column: x => x.hookId,
                        principalTable: "hooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sms_emails_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sms_emails_hookId",
                table: "sms_emails",
                column: "hookId");

            migrationBuilder.CreateIndex(
                name: "IX_sms_emails_siteId",
                table: "sms_emails",
                column: "siteId");
        }
    }
}
