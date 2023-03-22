using Client.Interfaces;

namespace Client.Menu;

public class PersonalOfficeMenu
{
    private readonly IPersonalOfficeHandler _personalOfficeHandler;

    public PersonalOfficeMenu(IPersonalOfficeHandler personalOfficeHandler)
    {
        _personalOfficeHandler = personalOfficeHandler;
    }

    public void Menu(string login)
    {
        while (true)
        {
            Console.Clear();
            _personalOfficeHandler.ViewInformation(login);

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    _personalOfficeHandler.ChangeName(login);
                    break;

                case '2':
                    _personalOfficeHandler.ChangePassword(login);
                    break;

                case '3':
                    _personalOfficeHandler.AddCash(login);
                    break;

                case '4':
                    return;

                default:
                    continue;
            }
        }
    }
}
