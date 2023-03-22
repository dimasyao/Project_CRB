using Client.Models;

namespace Client.Interfaces;

public interface IBettingRoomsHandler
{

    /// <returns>list of all aviable rooms to bet bu currency</returns>
    List<Room>? GetRooms(string curr);
}
