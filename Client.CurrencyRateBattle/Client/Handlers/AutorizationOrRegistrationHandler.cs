using Client.Common;
using Client.Common.CodeWays;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

internal class AutorizationOrRegistrationHandler : IAutorizationOrRegistration
{
    private readonly IInputItems _input;
    private readonly ILogger<AutorizationOrRegistrationHandler> _logger;
    private readonly User? _user;

    public AutorizationOrRegistrationHandler(IInputItems input, ILogger<AutorizationOrRegistrationHandler> logger)
    {
        _input = input;
        _logger = logger;
        _user = new User();
    }

    public User Autorization()
    {
        ConsoleColors.Green("\n\tLogIn panel\n");

        while (true)
        {
            #region Client part

            if (!InputPart(UserItem.Login, HttpWay.Login))
            {
                break;
            }

            if (!InputPart(UserItem.Pass, HttpWay.Login))
            {
                break;
            }

            #endregion

            if (_input.SendRequest(HttpWay.Login, _user!))
            {
                _logger.LogInformation("{Time}: AutorizationOrRegistrationHandler.Autorization() user login : {Login} logged in", DateTime.Now.ToShortTimeString(), _user!.Login);

                return _user!;
            }
            else if (UIMessanger.CancelationLoop())
            {
                break;
            }
        }

        return new User();
    }

    public User Registration()
    {
        while (true)
        {
            #region Client part

            if (!InputPart(UserItem.Login, HttpWay.Registration))
            {
                break;
            }

            if (!InputPart(UserItem.Pass, HttpWay.Registration))
            {
                break;
            }

            if (!InputPart(UserItem.Name, HttpWay.Registration))
            {
                break;
            }

            #endregion

            if (_input.SendRequest(HttpWay.Registration, _user!))
            {
                _logger.LogInformation("{Time}: AutorizationOrRegistrationHandler.Registration() user login : {Login} just registerted", DateTime.Now.ToShortTimeString(), _user!.Login);

                return _user!;
            }
            else if (UIMessanger.CancelationLoop())
            {
                break;
            }
        }

        return new User();
    }

    private bool InputPart(UserItem item, HttpWay httpWay)
    {
        while (true)
        {
            Console.Clear();
            ConsoleColors.Green($"\n\t{httpWay} panel\n");

            var validResult = _input.InputData(item);

            if (validResult.Item1.IsValid)
            {
                switch (item)
                {
                    case UserItem.Login:
                        _user!.Login = validResult.Item2;
                        break;

                    case UserItem.Pass:
                        _user!.Password = validResult.Item2;
                        break;

                    case UserItem.Name:
                        _user!.Name = validResult.Item2;
                        break;

                    default:
                        throw new ArgumentException(item.ToString());
                }

                return true;
            }
            else
            {
                if (httpWay == HttpWay.Registration)
                {
                    ConsoleColors.Red($"{validResult.Item1.Errors[0]}");
                }
                else
                {
                    ConsoleColors.Red("Invalid input");
                }

                if (UIMessanger.CancelationLoop())
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
