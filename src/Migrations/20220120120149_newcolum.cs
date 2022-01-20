using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class newcolum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BSN",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BSN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "AspNetUsers");
        }
    }
}
