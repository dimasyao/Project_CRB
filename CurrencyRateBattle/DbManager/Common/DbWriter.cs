using DbProvider.Data;
using DbProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System.Text.Json;

namespace DbProvider.Common;

public class DbWriter : IDbWriter
{
    private readonly CurrencyBattleDbContext _context;
    private readonly IDataReader _dataReader;

    public DbWriter(CurrencyBattleDbContext context, IDataReader dataReader)
    {
        _context = context;
        _dataReader = dataReader;
    }
    public void CreateUser(string login, string passwordHash, string name)
    {
        var newUser = new User
        {
            Login = login,
            Password = passwordHash,
            Name = name
        };

        _ = _context.TUsers.Add(newUser);
    }

    public void SetPasswordHash(string login, string password)
    {
        var user = _dataReader.GetUser(login);

        if (user != null)
            user.Password = password;
    }

    public void SetName(string login, string name)
    {
        var user = _dataReader.GetUser(login);

        if (user != null)
            user.Name = name;
    }
    public void AddBalance(string login, decimal balance)
    {
        var user = _dataReader.GetUser(login);

        if (user != null)
            user.Balance += balance;
    }

    public void AddGames(string login, int games)
    {
        var user = _dataReader.GetUser(login);

        if (user != null)
            user.TotalGames += games;
    }
    public void AddVictories(string login, int victories)
    {
        var user = _dataReader.GetUser(login);

        if (user != null)
            user.Victories += victories;
    }

    public void SetRoom(string roomId, string currency, int addMinutes)
    {
        var newRoom = new Room
        {
            RoomId = roomId,
            Currency = currency,
            Date = DateTime.UtcNow.AddMinutes(addMinutes)
        };

        _ = _context.TRooms.Add(newRoom);
    }

    public void DeleteRoom(Room room)
    {
        _ = _context.TRooms.Remove(room);
    }

    public void SetCurrency(string roomId, string currency)
    {
        var room = _dataReader.GetRoom(roomId);

        if (room != null)
            room.Currency = currency;
    }

    public void SetDate(string roomId, DateTime dateForSet)
    {
        var room = _dataReader.GetRoom(roomId);

        if (room != null)
            room.Date = dateForSet;
    }

    public void AddUserHistory(UserHistory toAdd, string login)
    {
        var existingHistory = _dataReader.GetUserHistory(login);

        if (existingHistory != null)
        {
            existingHistory.Add(toAdd);

            var user = _dataReader.GetUser(login);
            user.History = JsonSerializer.Serialize(existingHistory);
        }

        else
        {
            existingHistory = new List<UserHistory> { toAdd };

            var user = _dataReader.GetUser(login);
            user.History = JsonSerializer.Serialize(existingHistory);
        }

        AddUserNotification(toAdd, login);
    }

    public void AddBetsToRoom(Bet toAdd, string roomId)
    {
        var existingBets = _dataReader.GetBetsInRoom(roomId);

        if (existingBets != null)
        {
            existingBets.Add(toAdd);
            var room = _dataReader.GetRoom(roomId);
            room.Bets = JsonSerializer.Serialize(existingBets);
        }
        else
        {
            existingBets = new List<Bet> { toAdd };
            var room = _dataReader.GetRoom(roomId);
            room.Bets = JsonSerializer.Serialize(existingBets);
        }

    }

    public void UpdateRatings()
    {
        _ = _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"t_total_pLayed_rating\";" +
                                            " insert into \"t_total_pLayed_rating\"" +
                                            " select \"Login\",\"Name\", \"TotalGames\"" +
                                            " from t_users order by \"TotalGames\" desc" +
                                            " limit 100;");

        _ = _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"t_total_victories_rating\";" +
                                            "insert into \"t_total_victories_rating\" " +
                                            "select \"Login\",\"Name\", \"Victories\" " +
                                            "from t_users order by \"Victories\" desc " +
                                            "limit 100;");

        _ = _context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"t_winrate_rating\";" +
                                            " insert into \"t_winrate_rating\" " +
                                            "select \"Login\",\"Name\", ((\"Victories\"  * 100) / \"TotalGames\") " +
                                            "from t_users where \"TotalGames\" > 10 " +
                                            "order by ((\"Victories\"  * 100) / \"TotalGames\") desc " +
                                            "limit 100;");
    }

    public void Update()
    {
        _ = _context.SaveChanges();
    }

    private void AddUserNotification(UserHistory toAdd, string login)
    {
        var user = _dataReader.GetUser(login);

        if (!string.IsNullOrEmpty(user.Notification))
        {
            var notReadNotifications = JsonSerializer.Deserialize<List<UserHistory>>(user.Notification);

            notReadNotifications!.Add(toAdd);

            user.Notification = JsonSerializer.Serialize(notReadNotifications);
        }
        else
        {
            var notReadNotifications = new List<UserHistory> { toAdd };
            user.Notification = JsonSerializer.Serialize(notReadNotifications);
        }

    }
}
