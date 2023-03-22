using SharedModels.Models;
using SharedModels.Models.RatingEntities;

namespace DbProvider.Interfaces;

public interface IDataReader
{
    /// <summary>
    /// Check if user exist in db
    /// </summary>
    /// <param name="login">user login</param>
    /// <returns>search result</returns>
    bool IsUserExists(string login);

    /// <summary>
    /// Get user pass hash from db
    /// </summary>
    /// <param name="login">user login</param>
    /// <returns>hash of pass</returns>
    string? GetPasswordHash(string login);

    /// <summary>
    /// Use to get last notifications. Required to use _dbWriter.Update after geting!!
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    string? GetNewNotifications(string login);

    /// <summary>
    /// Get user history of games
    /// </summary>
    /// <param name="login">user login</param>
    /// <returns>list of games</returns>
    List<UserHistory> GetUserHistory(string login);

    /// <summary>
    /// Get all bets in actual room
    /// </summary>
    /// <param name="roomId">actual room</param>
    /// <returns>list of bets</returns>
    List<Bet> GetBetsInRoom(string roomId);

    /// <summary>
    /// Get room list
    /// </summary>
    /// <returns>list of room</returns>
    List<Room> GetRoomsList();

    /// <summary>
    /// Get actual user
    /// </summary>
    /// <param name="login">user login</param>
    /// <returns>user</returns>
    User GetUser(string login);

    /// <summary>
    /// Get actual room
    /// </summary>
    /// <param name="roomId">room id</param>
    /// <returns></returns>
    Room GetRoom(string roomId);

    /// <summary>
    /// Get players with the most games
    /// </summary>
    /// <returns>list of players</returns>
    List<TotalPlayed> GetTotalGamesRating();

    /// <summary>
    /// Get players with the most victories
    /// </summary>
    /// <returns>list of players</returns>
    List<TotalVictories> GetTotalVictoriesRating();

    /// <summary>
    /// Get players with the most winrate
    /// </summary>
    /// <returns>list of players</returns>
    List<Winrate> GetWinrateRating();
}
