using Client.Common;
using Client.Interfaces;

namespace Client.Menu;

public class RatingMenu
{
    private readonly IRatingHandler _ratingHnd;

    public RatingMenu(IRatingHandler ratingHnd)
    {
        _ratingHnd = ratingHnd;
    }

    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            UIMessanger.RatingMenu();

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    _ratingHnd.PrintRatingByTotalPlayed();
                    _ = Console.ReadKey();
                    break;

                case '2':
                    _ratingHnd.PrintRatingByVictories();
                    _ = Console.ReadKey();
                    break;

                case '3':
                    _ratingHnd.PrintRatingByWinrate();
                    _ = Console.ReadKey();
                    break;

                case '4':
                    return;

                default:
                    continue;
            }
        }
    }
}
