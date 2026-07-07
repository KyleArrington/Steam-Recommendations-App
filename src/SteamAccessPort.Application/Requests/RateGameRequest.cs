namespace SteamAccessPort.Application.Requests
{
    // JSON body for rating a game
    public class RateGameRequest
    {
        public string SteamUserId { get; set; } = string.Empty;
        public int SteamAppId { get; set; }

        public int OverallEnjoymentScore { get; set; }
        public int ReplayValueScore { get; set; }
        public int CurrentInterestScore { get; set; }

        public string? ReviewNote { get; set; }
    }
}