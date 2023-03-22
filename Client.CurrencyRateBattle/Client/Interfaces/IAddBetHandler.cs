using Client.Models;

namespace Client.Interfaces;

public interface IAddBetHandler
{
    /// <summary>
    /// Provides an ability to chose a room and make a bet
    /// </summary>
    /// <returns>Seccess(true) or failure (false)</returns>
    bool AddBet(List<Room> rooms, string login);
}
