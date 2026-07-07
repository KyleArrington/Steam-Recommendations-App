using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using SteamAccessPort.Application.Interfaces;
using SteamAccessPort.Application.Options;
using SteamAccessPort.Domain.Models;
using SteamAccessPort.Infrastructure.Steam;

namespace SteamAccessPort.Infrastructure
{
    // This Classes Job is to Map my Steam Objects to Steams Acutal Objects
    public class SteamApiGameService : ISteamGameService
    {
        private readonly HttpClient _httpClient;
        private readonly SteamOptions _steamOptions;

        public SteamApiGameService(HttpClient httpClient, IOptions<SteamOptions> steamOptions)
        {
            _httpClient = httpClient;
            _steamOptions = steamOptions.Value;
        }

        public async Task<IReadOnlyList<UserGame>> GetOwnedGamesAsync(string steamUserId)
        {
            var url =
                $"https://api.steampowered.com/IplayerService/GetOwnedGames/v1/" +
                $"?key={_steamOptions.ApiKey}" +
                $"&steamid={steamUserId}" +
                $"&include_appinfo=true" +
                $"&include_played_free_games=true";

            var steamResponse = await _httpClient.GetFromJsonAsync<SteamOwnedGamesResponse>(url);

            if(steamResponse?.Response?.Games == null)
            {
                return new List<UserGame>();
            }

            var games = steamResponse.Response.Games.Select(game => new UserGame
            {
                SteamUserId = steamUserId,
                SteamAppId = game.SteamAppId,
                Name = game.Name,
                PlaytimeMinutes = game.PlaytimeForever,
                PlaytimeTwoWeeksMinutes = game.PlaytimeTwoWeeks,
                IsOwned = true,
            }).ToList();

            return games;
        }
    }
}