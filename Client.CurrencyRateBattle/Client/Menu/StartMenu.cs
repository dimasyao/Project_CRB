using Client.Common;
using Client.Interfaces;
using Client.Models;

namespace Client.Menu;

public class StartMenu
{
    private readonly IAutorizationOrRegistration _autorizationOrRegistration;
    private readonly MainMenu _mainMenu;

    private User? _user;

    public StartMenu(IAutorizationOrRegistration autorizationOrRegistration, MainMenu mainMenu)
    {
        _autorizationOrRegistration = autorizationOrRegistration;
        _mainMenu = mainMenu;
    }
    public void Menu()
    {
        Console.SetWindowSize(150, 50);

        var flag = true;

        while (flag)
        {
            Console.Clear();
            UIMessanger.StartPage();

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    _user = _autorizationOrRegistration.Autorization();
                    break;

                case '2':
                    _user = _autorizationOrRegistration.Registration();
                    break;

                case '3':
                    flag = false;
                    break;

                default:
                    break;
            }

            if (_user is not null
                && !string.IsNullOrEmpty(_user!.Login))
            {
                _mainMenu.Menu(_user.Login);
            }

            _user = null;
        }
    }
}
