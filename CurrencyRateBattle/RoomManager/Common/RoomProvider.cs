using RoomManager.Interfaces;
using DbProvider.Interfaces;
using SharedModels.Models;

namespace RoomManager.Common;

public class RoomProvider : IRoomProvider
{
    private readonly IDataReader _dataReader;
    private readonly IDbWriter _dbWriter;
    private readonly IDetermWinner _determWinner;

    public RoomProvider(IDataReader dataReader, IDbWriter dbWriter, IDetermWinner determWinner)
    {
        _dataReader = dataReader;
        _dbWriter = dbWriter;
        _determWinner = determWinner;
    }

    public Task CreateNewRooms(List<string> currencies, int addMinutes)
    {
        foreach (var currency in currencies)
        {
            _dbWriter.SetRoom(Guid.NewGuid().ToString(), currency, addMinutes);
        }

        _dbWriter.Update();

        return Task.CompletedTask;
    }

    public Task TerminateRooms(List<string> currencies)
    {
        var rooms = _dataReader.GetRoomsList() ?? new List<Room>();

        for (var i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].Date <= DateTime.UtcNow + TimeSpan.FromMinutes(1))
            {
                var bets = _dataReader.GetBetsInRoom(rooms[i].RoomId ?? "null");

                if (bets != null)
                {
                    switch (DateTime.Now.Hour % 3)
                    {
                        case 2:
                            _determWinner.RoomWithApproximationAndAccurate(bets, rooms[i].Currency!);
                            break;

                        case 1:
                            _determWinner.RoomWithAccurate(bets, rooms[i].Currency!);
                            break;

                        case 0:
                            _determWinner.RoomWithApproximation(bets, rooms[i].Currency!);
                            break;

                        default:
                            break;
                    }
                }

                _dbWriter.DeleteRoom(rooms[i]);
            }
        }

        _dbWriter.Update();
        _dbWriter.UpdateRatings();

        return Task.CompletedTask;
    }
}
