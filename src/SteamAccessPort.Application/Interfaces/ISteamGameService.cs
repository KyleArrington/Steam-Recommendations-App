using SteamAccessPort.Domain.Models;

namespace SteamAccessPort.Application.Interfaces
{
    public interface ISteamGameService
    {
        Task<IReadOnlyList<UserGame>> GetOwnedGamesAsync(string steamUserId);
    }
}