using SharedModels.Models;

namespace DbProvider.Interfaces;

public interface IDbWriter
{
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="passwordHash">pass hash</param>
    /// <param name="name">user name</param>
    void CreateUser(string login, string passwordHash, string name);

    /// <summary>
    /// Change to user pass
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="password">user pass</param>
    void SetPasswordHash(string login, string password);

    /// <summary>
    /// Change to user pass
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="password">user pass</param>
    void SetName(string login, string name);

    /// <summary>
    /// Add to user balance
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="balance">adding balance</param>
    void AddBalance(string login, decimal balance);

    /// <summary>
    /// Add to user new games
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="games">user games</param>
    void AddGames(string login, int games);

    /// <summary>
    /// Add a new victory
    /// </summary>
    /// <param name="login">user login</param>
    /// <param name="victories">user victory</param>
    void AddVictories(string login, int victories);

    /// <summary>
    /// Add a new item in the user history
    /// </summary>
    /// <param name="toAdd">new history item</param>
    /// <param name="login">user login</param>
    void AddUserHistory(UserHistory toAdd, string login);

    /// <summary>
    /// Change to actual room
    /// </summary>
    /// <param name="roomId">room id</param>
    /// <param name="currency">curr</param>
    /// <param name="addMinutes"></param>
    void SetRoom(string roomId, string currency, int addMinutes);

    /// <summary>
    /// Delete the actual room
    /// </summary>
    /// <param name="room"></param>
    void DeleteRoom(Room room);

    /// <summary>
    /// Set the curr in the actual room
    /// </summary>
    /// <param name="roomId">room id</param>
    /// <param name="currency">actual curr</param>
    void SetCurrency(string roomId, string currency);

    /// <summary>
    /// Change a date in the actual room
    /// </summary>
    /// <param name="roomId">room id</param>
    /// <param name="dateForSet"></param>
    void SetDate(string roomId, DateTime dateForSet);

    /// <summary>
    /// Add bets to the actual room
    /// </summary>
    /// <param name="toAdd"></param>
    /// <param name="roomId"></param>
    void AddBetsToRoom(Bet toAdd, string roomId);

    /// <summary>
    /// Update ratings
    /// </summary>
    void UpdateRatings();

    /// <summary>
    /// update Db
    /// </summary>
    void Update();
}
