namespace SteamAccessPort.Domain.Models
{
    // Represents a user's rating for a game stored in SQL Server
    public class GameRating
    {
        public int Id { get; set; }

        public string SteamUserId { get; set; } = string.Empty;
        public int SteamAppId { get; set; }

        public int OverallEnjoymentScore { get; set; }
        public int ReplayValueScore { get; set; }
        public int CurrentInterestScore { get; set; }   

        public string? ReviewNote { get; set; }

        public DateTime RatedAtUtc { get; set; }
    }
}