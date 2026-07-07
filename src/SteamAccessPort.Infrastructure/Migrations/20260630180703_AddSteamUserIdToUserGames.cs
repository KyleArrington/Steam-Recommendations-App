using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAccessPort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSteamUserIdToUserGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SteamUserId",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SteamUserId",
                table: "UserGames");
        }
    }
}
