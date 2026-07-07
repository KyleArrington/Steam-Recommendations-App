namespace SteamAccessPort.Application.Responses
{
    public class GameRecommendationResponse
    {
        public int SteamAppId { get; set; }
        public string Name { get; set; } = string.Empty;

        public double PlaytimeHours { get; set; }

        public int OverallEnjoymentScore { get; set; }
        public int ReplayValueScore { get; set; }
        public int CurrentInterestScore { get; set; }

        public double RecommendationScore { get; set; }
        public string RecommendationCategory { get; set; } = string.Empty;

        public string? ReviewNote { get; set; }
    }
}