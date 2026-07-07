namespace SteamAccessPort.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int SteamAppId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PlaytimeMinutes { get; set; }
    }
}