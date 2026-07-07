namespace SteamAccessPort.Domain.Models
{
    // This class represents a steam game owned object
    public class UserGame
    {
        public int Id { get; set; }
        public string SteamUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int SteamAppId { get; set; }
        public int PlaytimeMinutes { get; set; }
        public int PlaytimeTwoWeeksMinutes { get; set; }
        public DateTime? LastPlayedAt { get; set; }
        public bool IsOwned { get; set; } = true;
    }
}