using Microsoft.AspNetCore.Mvc;
using SteamAccessPort.Application.Responses;
using SteamAccessPort.Application.Requests;
using SteamAccessPort.Application.Interfaces;
using SteamAccessPort.Domain.Models;

namespace SteamAccessPort.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamGamesController : ControllerBase
{
    private readonly ISteamGameService _steamGameService;
    private readonly IUserGameRepository _userGameRepository;

    public SteamGamesController(ISteamGameService steamGameService, IUserGameRepository userGameRepository)
    {
        _steamGameService = steamGameService;
        _userGameRepository = userGameRepository;
    }

    [HttpGet("owned/{steamUserId}")]
    public async Task<ActionResult<IReadOnlyList<OwnedGameResponse>>> GetOwnedGames(string steamUserId)
    {
        var games = await _steamGameService.GetOwnedGamesAsync(steamUserId);

        var response = games.Select(game => new OwnedGameResponse
        {
            SteamAppId = game.SteamAppId,
            Name = game.Name,
            PlaytimeMinutes = game.PlaytimeMinutes,
            PlaytimeTwoWeeksMinutes = game.PlaytimeTwoWeeksMinutes,
            PlaytimeHours = Math.Round(game.PlaytimeMinutes / 60.0, 1),
            IsOwned = game.IsOwned
        }).ToList();

        return Ok(response);
    }

    [HttpPost("sync/{steamUserId}")]
    public async Task<ActionResult> SyncOwnedGames(string steamUserId)
    {
        var games = await _steamGameService.GetOwnedGamesAsync(steamUserId);

        await _userGameRepository.SyncOwnedGamesAsync(games);

        return Ok(new
        {
            Message = "Steam library synced successfully.",
            SyncedCount = games.Count
        });
    }

    [HttpGet("saved/{steamUserId}")]
    public async Task<ActionResult<IReadOnlyList<UserGame>>> GetSavedGames(string steamUserId)
    {
        var games = await _userGameRepository.GetSavedGamesAsync(steamUserId);

        var response = games.Select(game => new OwnedGameResponse
        {
            SteamAppId = game.SteamAppId,
            Name = game.Name,
            PlaytimeMinutes = game.PlaytimeMinutes,
            PlaytimeTwoWeeksMinutes = game.PlaytimeTwoWeeksMinutes,
            PlaytimeHours = Math.Round(game.PlaytimeMinutes / 60.0, 1),
            IsOwned = game.IsOwned
        }).ToList();

        return Ok(response);
    }

    [HttpPost("ratings")]
    public async Task<ActionResult> RateGame(RateGameRequest request)
    {
        var rating = new GameRating
        {
            SteamUserId = request.SteamUserId,
            SteamAppId = request.SteamAppId,
            OverallEnjoymentScore = request.OverallEnjoymentScore,
            ReplayValueScore = request.ReplayValueScore,
            CurrentInterestScore = request.CurrentInterestScore,
            ReviewNote = request.ReviewNote
        };

        await _userGameRepository.SaveGameRatingAsync(rating);

        return Ok(new
        {
            message = "Rating Submitted"
        });
    }

    private static string GetRecommendationCategory(double recommendationScore)
    {
        if (recommendationScore >= 8.5)
        {
            return "Must Play";
        }
        if (recommendationScore >= 7.5)
        {
            return "Strong Recommendation";
        }
        if (recommendationScore >= 6.5)
        {
            return "Worth Playing Soon";
        }
        if (recommendationScore >= 5.0)
        {
            return "Maybe Later";
        }

        return "Low Priority";
    }


    [HttpGet("recommendations/{steamUserId}")]
    public async Task<ActionResult<IReadOnlyList<GameRecommendationResponse>>> GetRecommendations(string steamUserId)
    {
        var games = await _userGameRepository.GetSavedGamesAsync(steamUserId);
        var ratings = await _userGameRepository.GetGameRatingsAsync(steamUserId);

        var recommendations = games
            .Join(
                ratings,
                game => game.SteamAppId,
                rating => rating.SteamAppId,
                (game, rating) =>
                {
                    var recommendationScore =
                        rating.OverallEnjoymentScore * 0.4
                        + rating.ReplayValueScore * 0.3
                        + rating.CurrentInterestScore * 0.3;

                    return new GameRecommendationResponse
                    {
                        SteamAppId = game.SteamAppId,
                        Name = game.Name,
                        PlaytimeHours = Math.Round(game.PlaytimeMinutes / 60.0, 1),
                        OverallEnjoymentScore = rating.OverallEnjoymentScore,
                        ReplayValueScore = rating.ReplayValueScore,
                        CurrentInterestScore = rating.CurrentInterestScore,
                        RecommendationScore = Math.Round(recommendationScore, 1),
                        RecommendationCategory = GetRecommendationCategory(recommendationScore),
                        ReviewNote = rating.ReviewNote
                    };
                })
            .OrderByDescending(recommendation => recommendation.RecommendationScore)
            .ToList();

        return Ok(recommendations);
    }
}