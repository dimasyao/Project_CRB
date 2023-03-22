using DbProvider.Data;
using DbProvider.Interfaces;
using SharedModels.Models;
using SharedModels.Models.RatingEntities;
using System.Text.Json;

namespace DbProvider.Common;

public class DataReader : IDataReader
{
    private readonly CurrencyBattleDbContext _context;

    public DataReader(CurrencyBattleDbContext context)
    {
        _context = context;
    }

    public bool IsUserExists(string login)
    {
        return GetUser(login) != null;
    }

    public string? GetPasswordHash(string login)
    {
        return GetUser(login)?.Password;
    }

    public string? GetNewNotifications(string login)
    {
        var user = GetUser(login);

        var notificationsToReturn = string.Empty;

        if (user != null && !string.IsNullOrEmpty(user.Login))
        {
            notificationsToReturn = user.Notification;
            user.Notification = string.Empty;
        }

        return notificationsToReturn;
    }

    public bool IsRoomExists(string roomId)
    {
        return GetRoom(roomId) != null;
    }

    public List<UserHistory> GetUserHistory(string login)
    {
        var user = GetUser(login);

        if (user != null && !string.IsNullOrEmpty(user.History))
        {
            var history = JsonSerializer.Deserialize<List<UserHistory>>(user.History) ?? new List<UserHistory>();
            return history;
        }

        return new List<UserHistory>();
    }

    public List<Bet> GetBetsInRoom(string roomId)
    {
        var room = GetRoom(roomId);

        if (room != null && !string.IsNullOrEmpty(room.Bets))
        {
            var history = JsonSerializer.Deserialize<List<Bet>>(room.Bets) ?? new List<Bet>();
            return history;
        }

        return new List<Bet>();
    }

    public List<Room> GetRoomsList()
    {
        return _context.TRooms
            .ToList()
            ?? new List<Room>();
    }

    public User GetUser(string login)
    {
        return _context.TUsers
            .AsParallel()
            .FirstOrDefault(x => x.Login == login)
            ?? new User();
    }

    public Room GetRoom(string roomId)
    {
        return _context.TRooms
            .AsParallel()
            .FirstOrDefault(x => x.RoomId == roomId)
            ?? new Room();
    }

    public List<TotalPlayed> GetTotalGamesRating()
    {
        return _context.TotalPLayedRating
            .ToList()
            ?? new List<TotalPlayed>();
    }

    public List<TotalVictories> GetTotalVictoriesRating()
    {
        return _context.TotalVictoriesRating
            .ToList()
            ?? new List<TotalVictories>();
    }

    public List<Winrate> GetWinrateRating()
    {
        return _context.WinrateiesRating
            .ToList()
            ?? new List<Winrate>();
    }
}
