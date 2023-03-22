using DbProvider.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedModels.Models;

namespace Server.Controllers;

[ApiController]
public class RoomController : ControllerBase
{
    private readonly IDataReader _dataReader;
    private readonly IOptions<CustomConfiguration> _options;

    public RoomController(IDataReader dataReader, IOptions<CustomConfiguration> options)
    {
        _dataReader = dataReader;
        _options = options;
    }

    [Route("all")]
    [HttpGet]
    public List<Room> GetAllRooms()
    {
        return _dataReader.GetRoomsList()!;
    }

    [Route("currencies")]
    [HttpGet]
    public List<string> GetAllCurrencies()
    {
        return _options.Value.Currencies!;
    }
}
