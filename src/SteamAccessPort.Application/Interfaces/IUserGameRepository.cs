using SteamAccessPort.Domain.Models;
 
namespace SteamAccessPort.Application.Interfaces
{
    public interface IUserGameRepository
    {
        Task SyncOwnedGamesAsync(IReadOnlyList<UserGame> games);

        Task<IReadOnlyList<UserGame>> GetSavedGamesAsync(string steamUserId);

        Task SaveGameRatingAsync(GameRating rating);

        Task<IReadOnlyList<GameRating>> GetGameRatingsAsync(string steamUserId);
    }
}