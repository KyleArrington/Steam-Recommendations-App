using System.Text.Json.Serialization;

namespace SteamAccessPort.Infrastructure.Steam
{
    public class SteamOwnedGamesResponse
    {
        [JsonPropertyName("response")]
        public SteamOwnedGamesResponseBody Response { get; set; } = new();
    }

    public class SteamOwnedGamesResponseBody
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; set; }

        [JsonPropertyName("games")]
        public List<SteamOwnedGamesDto> Games { get; set; } = new();
    }

    // This class represents a literal steam game owned object
    public class SteamOwnedGamesDto
    {
        [JsonPropertyName("appid")]
        public int SteamAppId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever { get; set; }

        [JsonPropertyName("playtime_2weeks")]
        public int PlaytimeTwoWeeks { get; set; }
    }
}