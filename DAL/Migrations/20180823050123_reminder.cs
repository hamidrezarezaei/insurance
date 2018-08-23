using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class reminder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reminders",
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
                    fullName = table.Column<string>(nullable: true),
                    mobile = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    insuranceType = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reminders", x => x.id);
                    table.ForeignKey(
                        name: "FK_reminders_sites_siteId",
                        column: x => x.siteId,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reminders_siteId",
                table: "reminders",
                column: "siteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reminders");
        }
    }
}
