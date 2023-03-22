using System.Globalization;
using Client.Common;
using Client.Interfaces;
using Client.Models;

namespace Client.Menu;

public class RoomsMenu
{
    private readonly IBettingRoomsHandler _bettingRoomsHandler;
    private readonly IAddBetHandler _addBetHandler;
    private readonly ICurrencyListUpdateHandler _currencyListUpdateHandler;

    private List<Room>? _rooms;
    private string? _login;
    private char? _answer;

    public RoomsMenu(IBettingRoomsHandler bettingRoomsHandler, IAddBetHandler addBetHandler, ICurrencyListUpdateHandler currencyListUpdateHandler)
    {
        _bettingRoomsHandler = bettingRoomsHandler;
        _addBetHandler = addBetHandler;
        _currencyListUpdateHandler = currencyListUpdateHandler;
    }
    public void Menu(string login)
    {
        _login = login;
        var currencies = _currencyListUpdateHandler.GetCurrencyList();

        while (true)
        {
            Console.Clear();
            UIMessanger.RoomMenu(currencies);

            var choice = Console.ReadLine();

            if (int.TryParse(choice, out var index) && index > 0 && index <= currencies.Count)
            {
                RoomItem(currencies[index - 1]);
            }
            else if (choice == (currencies.Count + 1).ToString(CultureInfo.InvariantCulture))
            {
                break;
            }
        }
    }

    private void RoomItem(string curr)
    {
        var unOrderedRooms = _bettingRoomsHandler.GetRooms(curr) ?? new List<Room>();

        _rooms = unOrderedRooms.OrderBy(x => x.Date).ToList();

        UIMessanger.ListOfRooms(_rooms);
        ConsoleColors.Yellow("Would you like to bet? (Y \\ Any key)\n");

        _answer = Console.ReadKey().KeyChar;

        if (_answer is 'Y' or 'y')
        {
            if (_addBetHandler.AddBet(_rooms, _login!))
            {
                ConsoleColors.Green("Your bet accepted");
            }

            _ = Console.ReadKey();
        }
    }
}
