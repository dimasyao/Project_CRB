using DbProvider.Interfaces;
using FluentValidation.Results;
using Server.Common;
using Server.Interfaces;
using SharedModels.Models;

namespace Server.Services;

public class BetRegistryService : IBetRegistryService
{
    private readonly ILogger<BetRegistryService> _logger;
    private readonly IDataReader _dataReader;
    private readonly IDbWriter _dbWriter;
    private readonly BetValidator _validator;

    private ValidationResult _result;

    public BetRegistryService(ILogger<BetRegistryService> logger, IDataReader dataReader, IDbWriter dbWriter, BetValidator validator)
    {
        _logger = logger;
        _dataReader = dataReader;
        _dbWriter = dbWriter;
        _validator = validator;

        _result = new ValidationResult();
    }

    public ValidationResult AddBet(Bet bet, string roomId)
    {
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: BetRegistryService.AddBet() {bet} in Room Id: {roomId}");

        _result = _validator.Validate(bet);

        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: Bet is valid (fluent validation): {_result}");

        if (_result.IsValid)
        {
            IsCurrValid(bet.Currency!, roomId);
        }

        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: Bet is valid (private method): {_result}");

        if (_result.IsValid)
        {
            _dbWriter.AddBetsToRoom(bet, roomId);
            _dbWriter.AddBalance(bet.Login!, -bet.Sum);
            _dbWriter.Update();

            return _result;
        }

        return _result;
    }

    /// <summary>
    /// Validate currancy to roomId
    /// </summary>
    /// <param name="curr">curr</param>
    /// <param name="roomId">room</param>
    private void IsCurrValid(string curr, string roomId)
    {
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: In BetRegistryService.Validation()");

        foreach (var room in _dataReader.GetRoomsList()!)
        {
            if (room is not null && room.RoomId == roomId)
            {
                if (room.Currency != curr)
                {
                    _result.Errors.Add(new ValidationFailure(curr, "Invalid currancy!"));
                    return;
                }

                if ((DateTime.Now + new TimeSpan(3, 0, 0)) > room.Date)
                {
                    _result.Errors.Add(new ValidationFailure(roomId, "Game time is out!"));
                    return;
                }

                return;
            }
        }

        _result.Errors.Add(new ValidationFailure(roomId, "Invalid roomId!"));
        return;
    }
}
