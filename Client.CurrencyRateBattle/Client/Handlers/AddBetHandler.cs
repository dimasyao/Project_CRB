using Client.Common;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

public class AddBetHandler : IAddBetHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly ILogger<AddBetHandler> _logger;
    public AddBetHandler(IHttpClientWorker httpClientWorker, ILogger<AddBetHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _logger = logger;
    }

    public bool AddBet(List<Room> rooms, string login)
    {
        ConsoleColors.Yellow("\nEnter room number\n");
        var roomNumFlag = int.TryParse(Console.ReadLine(), out var roomNumber);

        ConsoleColors.Yellow("\nEnter value of currency you predict\n");
        var currencyValueFlag = decimal.TryParse(Console.ReadLine()!.Replace('.', ','), out var currencyValue);

        ConsoleColors.Yellow("\nEnter your bet value\n");
        var sumFlag = decimal.TryParse(Console.ReadLine()!.Replace('.', ','), out var sum);

        if (!roomNumFlag || !currencyValueFlag || !sumFlag)
        {
            ConsoleColors.Red("\nInvalid input, try again\n");
            return false;
        }

        if (roomNumber > rooms.Count || roomNumber < 1)
        {
            ConsoleColors.Red("\nNo such room\n");
            return false;
        }

        var room = rooms[roomNumber - 1];

        var postResult = _httpClientWorker.PostNewBet(room.RoomId!, new Bet
        {
            Login = login,
            Currency = room.Currency,
            Sum = sum,
            BetValue = currencyValue
        });

        _logger.LogInformation("{Time}: AddBetHandler.AddBet() login : {Login} in Room Id: {Id} with StatusCode {StatusCode}", DateTime.Now.ToShortTimeString(), login, room.RoomId, postResult.StatusCode);

        if (postResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        else
        {
            ConsoleColors.Red(postResult.Content.ReadAsStringAsync().Result);
            return false;
        }
    }
}
