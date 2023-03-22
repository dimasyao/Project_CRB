
using DbProvider.Interfaces;
using FluentValidation.Results;
using Server.Common;
using Server.Interfaces;
using SharedModels.Models;

namespace Server.Services;

public class RegisterService : IRegisterService
{
    private readonly ILogger<RegisterService> _logger;
    private readonly IDbWriter _dbWriter;
    private readonly UserRegistryValidator _validator;

    public RegisterService(ILogger<RegisterService> logger, IDbWriter dbWriter, UserRegistryValidator validator)
    {
        _logger = logger;
        _dbWriter = dbWriter;
        _validator = validator;
    }

    /// <summary>
    /// Validation and add new user to db
    /// </summary>
    /// <param name="user">New user</param>
    /// <returns></returns>
    public ValidationResult WriteNewUserAsync(User user)
    {
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: In WriteNewUserAsync. Validate: {user}");

        //validate login
        var result = _validator.Validate(user);

        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: User {user}, is valid: { result.IsValid}");

        //add new user
        if (result.IsValid)
        {
            _logger.LogInformation($"{ DateTime.Now.ToShortTimeString()}: Add new user in DB");

            _dbWriter.CreateUser(user.Login!, user.Password!, user.Name!);
            _dbWriter.Update();
        }

        return result;
    }
}
