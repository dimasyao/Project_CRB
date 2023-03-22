using DbProvider.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models.RatingEntities;

namespace Server.Controllers;

[ApiController]
public class RatingController : ControllerBase
{
    private readonly IDataReader _dataReader;

    public RatingController(IDataReader dataReader)
    {
        _dataReader = dataReader;
    }

    [Route("totalGames")]
    [HttpGet]
    public List<TotalPlayed> GetRatingByGames()
    {
        return _dataReader.GetTotalGamesRating();
    }

    [Route("totalWins")]
    [HttpGet]
    public List<TotalVictories> GetRatingByVictories()
    {
        return _dataReader.GetTotalVictoriesRating();
    }

    [Route("winRate")]
    [HttpGet]
    public List<Winrate> GetUserStatisticByWinRate()
    {
        return _dataReader.GetWinrateRating();
    }

}
