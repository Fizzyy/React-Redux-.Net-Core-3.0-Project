using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class deleteBanUserfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isUserBanned",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isUserBanned",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }
    }
}
