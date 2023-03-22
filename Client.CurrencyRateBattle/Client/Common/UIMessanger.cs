using System.Text;
using Client.Models;
using System.Globalization;
using System.Text.Json;

namespace Client.Common;

internal static class UIMessanger
{
    private static int _tableWidth;

    /// <summary>
    /// prints start page options
    /// </summary>
    public static void StartPage()
    {
        Console.Clear();
        ConsoleColors.Green("\n\tStart Menu\n");

        ConsoleColors.White("\n1. Log in\n" +
              "2. Registration of a new account\n" +
              "3. Exit");

        ConsoleColors.Yellow("\nTip: Press the menu item number on the keyboard!\n");
    }

    /// <summary>
    /// prints main menu options
    /// </summary>
    public static void MainMenu()
    {
        Console.Clear();
        ConsoleColors.Green("\n\tMain Menu\n");

        ConsoleColors.White("\n1. Place a bet\n" +
              "2. Personal office\n" +
              "3. Rating of players\n" +
              "4. Log out\n");

        ConsoleColors.Yellow("\nTip: Press the menu item number on the keyboard!\n");
    }

    /// <summary>
    /// prints list of aviable rooms to bet
    /// </summary>
    public static void ListOfRooms(List<Room> rooms)
    {
        Console.Clear();
        ConsoleColors.Green("\n\tAll rooms\n");

        if (rooms is not null && rooms.Count != 0)
        {
            PrintTableRooms(rooms);
        }

        ConsoleColors.Yellow("\nTip: Press the item number on the keyboard!\n");
    }

    /// <summary>
    /// prints menu of top 100 ratings options
    /// </summary>
    public static void RatingMenu()
    {
        Console.Clear();
        ConsoleColors.Green("\n\tRating Menu\n");

        ConsoleColors.White("\n1. Top 100 by total played games\n" +
              "2. Top 100 by victories\n" +
              "3. Top 100 by winrate (from 10 games played)\n" +
              "4. Back\n");

        ConsoleColors.Yellow("\nTip: Press the menu item number on the keyboard!\n");
    }

    /// <summary>
    /// prints menu of betting rooms options
    /// </summary>
    public static void RoomMenu(List<string> currencies)
    {
        Console.Clear();
        ConsoleColors.Green("\n\tRoom Menu\n");
        foreach (var currency in currencies)
        {
            ConsoleColors.White($"\n{currencies.IndexOf(currency) + 1}. {currency} Rooms");
        }

        ConsoleColors.White($"\n{currencies.Count + 1}. Back\n");
        ConsoleColors.Yellow("Tip: Press the menu item number on the keyboard!\n");
    }
    /// <summary>
    /// prints errors
    /// </summary>
    public static void Error(string err)
    {
        ConsoleColors.Red(err);
        PressAnyKey();
    }

    /// <summary>
    /// prints a tip to press any key
    /// </summary>
    public static void PressAnyKey()
    {
        ConsoleColors.Yellow("\nTip: Press any key.\n");
    }

    /// <summary>
    /// prints rating list table without its header
    /// </summary>
    public static void RatingList(List<Rating> ratingByPlayed)
    {
        foreach (var rating in ratingByPlayed)
        {
            if (rating.Name != null)
            {
                var position = ratingByPlayed.IndexOf(rating) + 1;
                var offset = 24 - rating.Name.Length - position.ToString(CultureInfo.InvariantCulture).Length;

                var offsetStr = new StringBuilder();

                for (var i = 0; i < offset; i++)
                {
                    _ = offsetStr.Append(' ');
                }

                ConsoleColors.Green($"\n{position}.{rating.Name}{offsetStr}|\t{rating.Games}\t|");
            }
        }

        ConsoleColors.Green("\n-----------------------------------------\n");
    }

    /// <summary>
    /// prints personal info and statistics, also prints some options in personal office
    /// </summary>
    public static void ViewUserInforationAndMenuPersonalOffice(User user, List<UserHistory>? userHistories)
    {
        Console.Clear();
        ConsoleColors.Green("\n\tPersonal office\n");

        ConsoleColors.White($"\n1. Change name      Your name: {user.Name}\n" +
                            $"2. Change password  Your login: {user.Login}\n" +
                            $"3. Add balance      Balance: {Math.Round(user.Balance, 2)}\n" +
                            $"4. Back             Total games: {user.TotalGames}\n" +
                            $"                    Victories: {user.Victories}\n");

        ConsoleColors.Yellow("\nYour bet history: \n");

        if (userHistories != null && userHistories.Count > 0)
        {
            PrintTableUserHistory(userHistories);
        }
        else
        {
            ConsoleColors.White("Your bet history is empty.");
        }
    }

    /// <summary>
    /// prints new notifications
    /// </summary>
    public static void ViewUserNotifications(List<UserHistory> notifications)
    {
        if (notifications != null && notifications.Count > 0)
        {
            ConsoleColors.Yellow("\nYour unread notifications: \n");
            PrintTableUserHistory(notifications);
        }
        else
        {
            ConsoleColors.Yellow("\nNo new notifications for you\n");
        }
    }

    /// <returns>if escape was pressed</returns>
    public static bool CancelationLoop()
    {
        ConsoleColors.Yellow("\nTo try again, press any button. Press \"Esc\" to finish.");
        return Console.ReadKey().Key == ConsoleKey.Escape;
    }
    /// <summary>
    /// prints aviable to bet rooms
    /// </summary>
    private static void PrintTableRooms(List<Room> rooms)
    {
        _tableWidth = 45;

        PrintLine();
        PrintRow("Index", "Date", "Currancy", "Bets");
        PrintLine();

        foreach (var item in rooms)
        {
            var bets = new List<Bet>();

            switch (item.Date.Hour % 3)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case 2:

                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrEmpty(item.Bets))
                bets = JsonSerializer.Deserialize<List<Bet>>(item.Bets!);

            var date = item.Date.AddHours(-(DateTime.UtcNow - DateTime.Now).Hours);

            PrintRow($"{rooms.IndexOf(item) + 1}", date.ToString("dddd, dd MMMM hh:mm tt", new CultureInfo("en-US")), item.Currency!, $"{bets!.Count}");
        }

        PrintLine();
        Console.WriteLine();

        ConsoleColors.Cyan("Room with approximation. Wins 30% of nearby bets. \n Distribution of winnings depending on the size of your bet.\n");
        Console.WriteLine();
        ConsoleColors.DarkYellow("Room with accurate. Only bets that are exactly on target are won.\n Distribution of winnings depending on the size of your bet.\n");
        Console.WriteLine();
        ConsoleColors.White("Room with accurate and approximation. Bets that hit the target distribute 60% of pool, approximate rates are the remainder. \n If no exact bets have been made, the pool is distributed among 30% of the nearest bets.\n Distribution of winnings depending on the size of your bet.\n");
    }

    /// <summary>
    /// prints user history table
    /// </summary>
    private static void PrintTableUserHistory(List<UserHistory> history)
    {
        _tableWidth = 95;

        PrintLine();
        PrintRow("#", "Date", "Currancy", "Prediction", "Bet", "Wining", "Status game");
        PrintLine();

        foreach (var item in history)
        {
            var won = item.HasWon ? "Won" : "Lose";

            Console.ForegroundColor = item.HasWon
                ? ConsoleColor.Green
                : ConsoleColor.Red;

            PrintRow($"{history.IndexOf(item) + 1}", item.Date.ToString("dddd, dd MMMM hh:mm tt", new CultureInfo("en-US")), item.Currency!, $"{item.Prediction}", $"{item.AmountOfBet}", $"{Math.Round(item.AmountOfWining, 2)}", won);
        }

        PrintLine();
        Console.WriteLine();
    }

    /// <summary>
    /// prints horisontal line from dashes -----
    /// </summary>
    private static void PrintLine()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(new string('-', _tableWidth + 15));
    }

    /// <summary>
    /// prints row in a table
    /// </summary>
    private static void PrintRow(params string[] columns)
    {
        var width = (_tableWidth - columns.Length) / columns.Length;
        var row = "|";

        for (var i = 0; i < columns.Length; i++)
        {
            if (i == 0)
            {
                row += AlignCentre(columns[i], width / 2) + "|";
            }
            else if (i == 1)
            {
                row += AlignCentre(columns[i], width * 3) + "|";
            }
            else
            {
                row += AlignCentre(columns[i], width) + "|";
            }
        }

        Console.WriteLine(row);
    }

    /// <summary>
    /// makes text in table column written in the middle
    /// </summary>
    private static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? string.Concat(text.AsSpan(0, width - 3), "...") : text;

        return string.IsNullOrEmpty(text) ? new string(' ', width) : text.PadRight(width - ((width - text.Length) / 2)).PadLeft(width);
    }
}
