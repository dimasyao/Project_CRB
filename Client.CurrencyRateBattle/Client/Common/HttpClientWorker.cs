using System.Text;
using System.Text.Json;
using Client.Interfaces;
using Client.Models;

namespace Client.Common;

internal class HttpClientWorker : IHttpClientWorker, IDisposable
{
    private readonly HttpClient _httpClient;

    public HttpClientWorker(string connection)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(connection)
        };
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public HttpResponseMessage GetRatingByGames()
    {
        return _httpClient.GetAsync("/totalGames").Result;
    }

    public HttpResponseMessage GetRatingByVictories()
    {
        return _httpClient.GetAsync("/totalWins").Result;
    }

    public HttpResponseMessage GetRatingByWinRate()
    {
        return _httpClient.GetAsync("/winRate").Result;
    }

    public HttpResponseMessage GetAllRooms()
    {
        return _httpClient.GetAsync("/all").Result;
    }
    public HttpResponseMessage GetCurrencyList()
    {
        return _httpClient.GetAsync("/currencies").Result;
    }

    public HttpResponseMessage GetInformationAboutUser(string login)
    {
        var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/information", content).Result;
    }

    public HttpResponseMessage PostNameToUser(string login, string name)
    {
        var content = new StringContent(JsonSerializer.Serialize(new KeyValuePair<string, string>(login, name)), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/newname", content).Result;
    }

    public HttpResponseMessage PostPasswordToUser(string login, string password)
    {
        var content = new StringContent(JsonSerializer.Serialize(new KeyValuePair<string, string>(login, password)), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/newpassword", content).Result;
    }

    public HttpResponseMessage PostCash(string login)
    {
        var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/addcash", content).Result;
    }

    public HttpResponseMessage GetNewNotifications(string login)
    {
        var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/notification", content).Result;
    }

    public HttpResponseMessage PostLogin(User user)
    {
        var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/signin", content).Result;
    }

    public HttpResponseMessage PostRegistration(User user)
    {
        var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/registry", content).Result;
    }

    public HttpResponseMessage GetUserHistory(string login)
    {
        var content = new StringContent(login, Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/user/history", content).Result;
    }

    public HttpResponseMessage PostNewBet(string roomId, Bet bet)
    {
        var content = new StringContent(JsonSerializer.Serialize(new KeyValuePair<string, Bet>(roomId, bet)), Encoding.UTF8, "application/json");

        return _httpClient.PostAsync("/new_bet", content).Result;
    }
}
