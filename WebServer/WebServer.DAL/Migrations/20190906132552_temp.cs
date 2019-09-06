using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "GameOfferAmount",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameOfferAmount",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
