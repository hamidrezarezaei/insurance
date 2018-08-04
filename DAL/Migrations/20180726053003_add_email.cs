using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class add_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "emails",
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
                    emailAddress = table.Column<string>(nullable: true),
                    hookid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_emails_hooks_hookid",
                        column: x => x.hookid,
                        principalTable: "hooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emails_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emails_hookid",
                table: "emails",
                column: "hookid");

            migrationBuilder.CreateIndex(
                name: "IX_emails_siteId",
                table: "emails",
                column: "siteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emails");
        }
    }
}
