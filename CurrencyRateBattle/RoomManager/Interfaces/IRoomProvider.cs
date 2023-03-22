namespace RoomManager.Interfaces;

public interface IRoomProvider
{
    /// <summary>
    /// Create new rooms
    /// </summary>
    /// <param name="currencies">list of create curr</param>
    /// <param name="addMinutes">create time interval</param>
    /// <returns></returns>
    Task CreateNewRooms(List<string> currencies, int addMinutes);

    /// <summary>
    /// Delete time out rooms
    /// </summary>
    /// <param name="currencies">list of curr</param>
    /// <returns></returns>
    Task TerminateRooms(List<string> currencies);

}
