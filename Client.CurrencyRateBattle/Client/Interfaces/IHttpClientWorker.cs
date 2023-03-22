using Client.Models;

namespace Client.Interfaces;

public interface IHttpClientWorker
{
    /// <returns>Returns top 100 by total played games</returns>
    HttpResponseMessage GetRatingByGames();

    /// <returns>Returns top 100 by victories</returns>
    HttpResponseMessage GetRatingByVictories();

    /// <returns>Returns top 100 by winrate</returns>
    HttpResponseMessage GetRatingByWinRate();

    /// <returns>Returns modern aviable to bet currencies</returns>
    HttpResponseMessage GetCurrencyList();

    /// <returns>Returns list of rooms aviable to bet</returns>
    HttpResponseMessage GetAllRooms();

    /// <summary>
    /// Changes name of user in database
    /// </summary>
    HttpResponseMessage PostNameToUser(string login, string name);

    /// <summary>
    /// Changes password of user in database
    /// </summary>
    HttpResponseMessage PostPasswordToUser(string login, string password);

    /// <summary>
    /// Changes balance of user in database
    /// </summary>
    HttpResponseMessage PostCash(string login);

    /// <returns>user`s statistics as number of games, victories etc</returns>
    HttpResponseMessage GetInformationAboutUser(string login);

    /// <returns>user`s notifications(history about bets which were calculated when user was offline)</returns>
    HttpResponseMessage GetNewNotifications(string login);

    /// <summary>
    /// sign in user on server
    /// </summary>
    HttpResponseMessage PostLogin(User user);

    /// <summary>
    /// creates new user in database
    /// </summary>
    HttpResponseMessage PostRegistration(User user);

    /// <returns>History of user (played games for all time)</returns>
    HttpResponseMessage GetUserHistory(string login);

    /// <summary>
    /// Placing of a new bet to some room
    /// </summary>
    HttpResponseMessage PostNewBet(string roomId, Bet bet);
}
