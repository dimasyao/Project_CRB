using Client.Common;
using Client.Interfaces;

namespace Client.Menu;

public class MainMenu
{
    private readonly RatingMenu _ratingMenu;
    private readonly RoomsMenu _roomsMenu;
    private readonly PersonalOfficeMenu _personalOffice;
    private readonly INotificationsHandler _notificationsHandler;
    public MainMenu(RatingMenu ratingMenu, INotificationsHandler notificationsHandler, RoomsMenu roomsmenu, PersonalOfficeMenu personalOffice)
    {
        _ratingMenu = ratingMenu;
        _roomsMenu = roomsmenu;
        _personalOffice = personalOffice;
        _notificationsHandler = notificationsHandler;
    }
    public void Menu(string login)
    {
        while (true)
        {
            Console.Clear();
            UIMessanger.MainMenu();

            _notificationsHandler.PrintNotification(login);

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    _roomsMenu.Menu(login);
                    break;

                case '2':
                    _personalOffice.Menu(login);
                    break;

                case '3':
                    _ratingMenu.Menu();
                    break;

                case '4':
                    return;

                default:
                    continue;
            }
        }
    }
}
