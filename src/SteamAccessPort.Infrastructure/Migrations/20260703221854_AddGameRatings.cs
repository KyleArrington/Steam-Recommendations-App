using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAccessPort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaytimeTwoWeeks",
                table: "UserGames",
                newName: "PlaytimeTwoWeeksMinutes");

            migrationBuilder.CreateTable(
                name: "GameRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SteamUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SteamAppId = table.Column<int>(type: "int", nullable: false),
                    OverallEnjoymentScore = table.Column<int>(type: "int", nullable: false),
                    ReplayValueScore = table.Column<int>(type: "int", nullable: false),
                    CurrentInterestScore = table.Column<int>(type: "int", nullable: false),
                    ReviewNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRatings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRatings");

            migrationBuilder.RenameColumn(
                name: "PlaytimeTwoWeeksMinutes",
                table: "UserGames",
                newName: "PlaytimeTwoWeeks");
        }
    }
}
