using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class NewFieldOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfOffer",
                table: "Offers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfOffer",
                table: "Offers");
        }
    }
}
