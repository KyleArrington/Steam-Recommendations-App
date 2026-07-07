using Microsoft.EntityFrameworkCore;
using SteamAccessPort.Domain.Models;

namespace SteamAccessPort.Infrastructure.Data
{
    public class SteamGamesDbContext : DbContext
    {
        public SteamGamesDbContext(DbContextOptions<SteamGamesDbContext> options) : base(options)
        {
        }

        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<GameRating> GameRatings { get; set; }
    }
}