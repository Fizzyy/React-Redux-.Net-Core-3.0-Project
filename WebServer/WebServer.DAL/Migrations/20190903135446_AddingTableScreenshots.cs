using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.DAL.Migrations
{
    public partial class AddingTableScreenshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfVotes",
                table: "GameFinalScores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GamesScreenshots",
                columns: table => new
                {
                    GameID = table.Column<string>(nullable: false),
                    GameScreenshotReference1 = table.Column<string>(nullable: true),
                    GameScreenshotReference2 = table.Column<string>(nullable: true),
                    GameScreenshotReference3 = table.Column<string>(nullable: true),
                    GameDescriptionBackground = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesScreenshots", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_GamesScreenshots_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesScreenshots");

            migrationBuilder.DropColumn(
                name: "AmountOfVotes",
                table: "GameFinalScores");
        }
    }
}
