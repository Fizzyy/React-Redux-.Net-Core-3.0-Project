using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class isActiveGameAndMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "MoneyKeys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Games",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "MoneyKeys");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Games");
        }
    }
}
