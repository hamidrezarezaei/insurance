using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class email_refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
  
            migrationBuilder.RenameColumn(
                name: "text",
                table: "emails",
                newName: "subject");
  
  
            migrationBuilder.AddColumn<string>(
                name: "body",
                table: "emails",
                nullable: true);

          }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
  
   
            migrationBuilder.DropColumn(
                name: "body",
                table: "emails");

 
  
            migrationBuilder.RenameColumn(
                name: "subject",
                table: "emails",
                newName: "text");
 
         }
    }
}
