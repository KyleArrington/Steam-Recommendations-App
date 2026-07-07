using Microsoft.EntityFrameworkCore;
using SteamAccessPort.Application.Interfaces;
using SteamAccessPort.Domain.Models;

namespace SteamAccessPort.Infrastructure.Data
{
    // This class is an Upsert-style sync, specially for UserGame objects.
    public class UserGameRepository : IUserGameRepository
    {
        private readonly SteamGamesDbContext _dbContext;

        public UserGameRepository(SteamGamesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SyncOwnedGamesAsync(IReadOnlyList<UserGame> games)
        {
            foreach (var game in games)
            {
                var existingGame = await _dbContext.UserGames
                    .FirstOrDefaultAsync( x => 
                        x.SteamUserId == game.SteamUserId &&
                        x.SteamAppId == game.SteamAppId);
                
                if (existingGame == null)
                {
                    _dbContext.UserGames.Add(game);
                }
                else
                {
                    existingGame.Name = game.Name;
                    existingGame.PlaytimeMinutes = game.PlaytimeMinutes;
                    existingGame.PlaytimeTwoWeeksMinutes = game.PlaytimeTwoWeeksMinutes;
                    existingGame.IsOwned = game.IsOwned;
                    existingGame.LastPlayedAt = game.LastPlayedAt;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<UserGame>> GetSavedGamesAsync(string steamUserId)
        {
            return await _dbContext.UserGames
                .Where(game => game.SteamUserId == steamUserId)
                .OrderByDescending(game => game.PlaytimeMinutes)
                .ToListAsync();
        }

        public async Task SaveGameRatingAsync(GameRating rating)
        {
            var existingRating = await _dbContext.GameRatings
                .FirstOrDefaultAsync(x =>
                    x.SteamUserId == rating.SteamUserId &&
                    x.SteamAppId == rating.SteamAppId);
            if (existingRating == null)
            {
                rating.RatedAtUtc = DateTime.UtcNow;
                _dbContext.GameRatings.Add(rating);
            }
            else
            {
                existingRating.OverallEnjoymentScore = rating.OverallEnjoymentScore;
                existingRating.ReplayValueScore = rating.ReplayValueScore;
                existingRating.CurrentInterestScore = rating.CurrentInterestScore;
                existingRating.ReviewNote = rating.ReviewNote;
                existingRating.RatedAtUtc = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<GameRating>> GetGameRatingsAsync(string steamUserId)
        {
            return await _dbContext.GameRatings
                .Where(rating => rating.SteamUserId == steamUserId)
                .ToListAsync();
        }    
    }
}