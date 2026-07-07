namespace SteamAccessPort.Application.Responses
{
    // This class represents what the API returns to the browser/client.
    public class OwnedGameResponse
    {
        public int SteamAppId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PlaytimeMinutes { get; set; }
        public int PlaytimeTwoWeeksMinutes { get; set; }
        public double PlaytimeHours { get; set; }
        public bool IsOwned { get; set; }   
    }
}