using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class AddUserAvatarAndBanTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BannedUsers",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    BanReason = table.Column<string>(nullable: true),
                    BanDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedUsers", x => x.Username);
                    table.ForeignKey(
                        name: "FK_BannedUsers_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedUsers");

            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Users");
        }
    }
}
