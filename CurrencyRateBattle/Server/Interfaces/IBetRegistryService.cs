using FluentValidation.Results;
using SharedModels.Models;

namespace Server.Interfaces;

public interface IBetRegistryService
{
    /// <summary>
    /// Validation and add new bet in db if bet is valid.
    /// </summary>
    /// <param name="bet">new bet</param>
    /// <param name="roomId">actual room</param>
    /// <returns>validation result with errors content</returns>
    ValidationResult AddBet(Bet bet, string roomId);
}
