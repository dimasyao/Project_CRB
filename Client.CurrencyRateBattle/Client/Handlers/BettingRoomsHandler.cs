using System.Text.Json;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;

namespace Client.Handlers;

public class BettingRoomsHandler : IBettingRoomsHandler
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly ILogger<BettingRoomsHandler> _logger;

    public BettingRoomsHandler(IHttpClientWorker httpClientWorker, ILogger<BettingRoomsHandler> logger)
    {
        _httpClientWorker = httpClientWorker;
        _logger = logger;
    }

    public List<Room>? GetRooms(string curr)
    {
        var responce = _httpClientWorker.GetAllRooms().Content.ReadAsStringAsync().Result;

        if (!string.IsNullOrWhiteSpace(responce))
        {
            var rooms = JsonSerializer.Deserialize<List<Room>>(responce)
                                      ?.Where(r => r.Currency == curr)
                                      .ToList();
            return rooms;
        }

        _logger.LogInformation("{Time}: BettingRoomsHandler.GetRooms() got rooms with currency : {Currency}", DateTime.Now.ToShortTimeString(), curr);

        return new List<Room>();
    }
}
