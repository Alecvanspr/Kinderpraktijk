using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class NewId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_srcUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_srcUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "srcUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SpecialistId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "srcUsersrcUser",
                columns: table => new
                {
                    ChilderenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_srcUsersrcUser", x => new { x.ChilderenId, x.ClientsId });
                    table.ForeignKey(
                        name: "FK_srcUsersrcUser_AspNetUsers_ChilderenId",
                        column: x => x.ChilderenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_srcUsersrcUser_AspNetUsers_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_srcUsersrcUser_ClientsId",
                table: "srcUsersrcUser",
                column: "ClientsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "srcUsersrcUser");

            migrationBuilder.DropColumn(
                name: "SpecialistId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "srcUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_srcUserId",
                table: "AspNetUsers",
                column: "srcUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_srcUserId",
                table: "AspNetUsers",
                column: "srcUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
