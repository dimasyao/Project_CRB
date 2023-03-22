using System.Text.Json;
using Client.Common;
using Client.Common.CodeWays;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

public class PersonalOfficeHandler : IPersonalOfficeHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly IInputItems _inputItems;
    private readonly ILogger<PersonalOfficeHandler> _logger;

    public PersonalOfficeHandler(IHttpClientWorker httpClientWorker, IInputItems inputItems, ILogger<PersonalOfficeHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _inputItems = inputItems;
        _logger = logger;
    }

    public void ViewInformation(string login)
    {
        var responce = _httpClientWorker.GetInformationAboutUser(login).Content.ReadAsStringAsync().Result;

        if (responce != null && !string.IsNullOrWhiteSpace(responce))
        {
            var user = JsonSerializer.Deserialize<User>(responce);
            var userHistory = new List<UserHistory>();

            if (user!.History != null && !string.IsNullOrEmpty(user.History))
            {
                userHistory = JsonSerializer.Deserialize<List<UserHistory>>(user.History);
            }

            UIMessanger.ViewUserInforationAndMenuPersonalOffice(user, userHistory);

            _logger.LogInformation("{Time}: PersonalOfficeHandler.ViewInformation() personal info for user {Login}", DateTime.Now.ToShortTimeString(), login);
        }
        else
        {
            UIMessanger.Error("The server is not available. Please try again later.");

            _logger.LogInformation("{Time}: PersonalOfficeHandler.ViewInformation() printed \"Error, Server not aviable\" for user {Login}", DateTime.Now.ToShortTimeString(), login);
        }

        UIMessanger.PressAnyKey();
    }

    public void ChangeName(string login)
    {
        while (true)
        {
            Console.Clear();
            ConsoleColors.Green("\n\tChange name panel\n");

            var result = _inputItems.InputData(UserItem.Name);

            if (result.Item1.IsValid
                && _inputItems.SendRequest(HttpWay.ChangeName, new User
                {
                    Name = result.Item2,
                    Login = login
                }))
            {
                ConsoleColors.Green("Your name has been successfully changed.");

                _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, changed his Name", DateTime.Now.ToShortTimeString(), login);

                UIMessanger.PressAnyKey();
                _ = Console.ReadKey();

                return;
            }
            else
            {
                ConsoleColors.Red($"{result.Item1.Errors[0]}");
                UIMessanger.PressAnyKey();

                _ = Console.ReadKey();

                _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, tryed to change his Name, but smth went wrong", DateTime.Now.ToShortTimeString(), login);

                if (UIMessanger.CancelationLoop())
                {
                    return;
                }
            }
        }
    }

    public void ChangePassword(string login)
    {
        while (true)
        {
            Console.Clear();
            ConsoleColors.Green("\n\tChange password panel\n");

            var result = _inputItems.InputData(UserItem.Pass);

            if (result.Item1.IsValid
                && _inputItems.SendRequest(HttpWay.ChangePassword, new User
                {
                    Password = result.Item2,
                    Login = login
                }))
            {
                ConsoleColors.Green("Your password has been successfully changed.");

                _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, changed his Password", DateTime.Now.ToShortTimeString(), login);

                UIMessanger.PressAnyKey();
                _ = Console.ReadKey();

                return;
            }
            else
            {
                ConsoleColors.Red($"{result.Item1.Errors[0]}");
                UIMessanger.PressAnyKey();

                _ = Console.ReadKey();

                _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, tryed to change his Password, but smth went wrong", DateTime.Now.ToShortTimeString(), login);

                if (UIMessanger.CancelationLoop())
                {
                    break;
                }
            }
        }
    }

    public void AddCash(string login)
    {
        Console.Clear();
        ConsoleColors.Green("\n\tBalance replenishment panel\n");

        if (_httpClientWorker.PostCash(login).IsSuccessStatusCode)
        {
            ConsoleColors.Green("\nYour balance successfully updated.\n");

            _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, added himself money", DateTime.Now.ToShortTimeString(), login);
        }
        else
        {
            ConsoleColors.Red("\nSorry, but you have enough money on your balance or server is currently unavailable.\n");

            _logger.LogInformation("{Time}: PersonalOfficeHandler.ChangeName() user : {Login}, tryed to add himself money, but smth went wrong", DateTime.Now.ToShortTimeString(), login);
        }

        UIMessanger.PressAnyKey();

        _ = Console.ReadLine();
    }
}
