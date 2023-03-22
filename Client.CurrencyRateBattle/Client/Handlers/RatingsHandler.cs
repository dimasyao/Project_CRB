using System.Text.Json;
using Client.Common;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;
public class RatingsHandler : IRatingHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly ILogger<RatingsHandler> _logger;

    public RatingsHandler(IHttpClientWorker httpClientWorker, ILogger<RatingsHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _logger = logger;
    }

    public void PrintRatingByTotalPlayed()
    {
        ConsoleColors.Yellow("\n-----------------------------------------");
        ConsoleColors.Yellow("\n           Name          |\tGames\t|");
        ConsoleColors.Green("\n-----------------------------------------");

        var ratingByPlayedResponce = _httpClientWorker.GetRatingByGames().Content.ReadAsStringAsync().Result;

        GetRating(ratingByPlayedResponce);

        _logger.LogInformation("{Time}: RatingsHandler.PrintRatingByTotalPlayed() printed  top 100 rating by total played", DateTime.Now.ToShortTimeString());
    }

    public void PrintRatingByVictories()
    {
        ConsoleColors.Yellow("\n-----------------------------------------");
        ConsoleColors.Yellow("\n           Name          |   Victories  |");
        ConsoleColors.Green("\n-----------------------------------------");

        var ratingByPlayedResponce = _httpClientWorker.GetRatingByVictories().Content.ReadAsStringAsync().Result;

        GetRating(ratingByPlayedResponce);

        _logger.LogInformation("{Time}: RatingsHandler.PrintRatingByVictories() printed  top 100 rating by victories", DateTime.Now.ToShortTimeString());
    }

    public void PrintRatingByWinrate()
    {
        ConsoleColors.Yellow("\n!!! In there are only people who played 10 or more games !!!");
        ConsoleColors.Yellow("\n-----------------------------------------");
        ConsoleColors.Yellow("\n           Name          |   Winrate %  |");
        ConsoleColors.Green("\n-----------------------------------------");

        var ratingByPlayedResponce = _httpClientWorker.GetRatingByWinRate().Content.ReadAsStringAsync().Result;

        GetRating(ratingByPlayedResponce);

        _logger.LogInformation("{Time}: RatingsHandler.PrintRatingByWinrate() printed  top 100 rating by victories", DateTime.Now.ToShortTimeString());
    }
    private void GetRating(string ratingResponce)
    {
        var ratingByPlayed = JsonSerializer.Deserialize<List<Rating>>(ratingResponce);

        if (ratingByPlayed != null)
        {
            UIMessanger.RatingList(ratingByPlayed);
        }
    }
}
