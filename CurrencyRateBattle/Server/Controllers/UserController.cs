using DbProvider.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Server.Common;
using System.Text.Json;

namespace Server.Controllers;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IDataReader _dataReader;
    private readonly IDbWriter _dbWriter;
    private readonly NameValidator _nameValidator;

    public UserController(ILogger<UserController> logger, IDataReader dataReader, IDbWriter dbWriter, NameValidator nameValidator)
    {
        _logger = logger;
        _dataReader = dataReader;
        _dbWriter = dbWriter;
        _nameValidator = nameValidator;
    }

    [Route("information")]
    [HttpPost]
    public IActionResult GetUser([FromBody] string login)
    {
        _logger.LogInformation("In GetUser");

        // validate login and check if the user exists
        if (SimpleLoginValidation(login))
        {
            _logger.LogWarning(new InvalidDataException(login), "Invalid input data");

            return BadRequest();
        }

        //get info about user from db
        var user = _dataReader.GetUser(login!);
        user!.Password = string.Empty;

        return Ok(JsonSerializer.Serialize(user));
    }

    [Route("notification")]
    [HttpPost]
    public IActionResult GetNotifications([FromBody] string login)
    {
        _logger.LogInformation("In GetNotifications");

        // validate login and check if the user exists
        if (SimpleLoginValidation(login))
        {
            _logger.LogWarning(new InvalidDataException(login), "Invalid input data");

            return BadRequest();
        }

        _logger.LogInformation("Call to DB. GetNewNotifications().");

        //get notification to user from db
        var userNotification = _dataReader.GetNewNotifications(login);
        _dbWriter.Update();

        return Ok(userNotification);
    }

    [Route("newname")]
    [HttpPost]
    public IActionResult PostNewName([FromBody] KeyValuePair<string, string> loginName)
    {
        _logger.LogInformation("In PostNewName");

        var result = _nameValidator.Validate(loginName.Value);

        // validate login and check if the user exists
        if (!result.IsValid || !_dataReader.IsUserExists(loginName.Key))
        {
            _logger.LogWarning(new InvalidDataException(loginName.ToString()), "Invalid input data");

            result.Errors.Add(new ValidationFailure("User", "User can not be null!"));

            return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
        }

        //update name to user
        _dbWriter.SetName(loginName.Key, loginName.Value);
        _dbWriter.Update();

        return Ok();
    }

    [Route("newpassword")]
    [HttpPost]
    public IActionResult PostNewPassword([FromBody] KeyValuePair<string, string> loginPassword)
    {
        _logger.LogInformation("In PostNewPassword");

        // validate login and check if the user exists
        if (SimpleLoginValidation(loginPassword.Key))
        {
            _logger.LogWarning(new InvalidDataException(loginPassword.ToString()), $"Invalid input login/pass");

            return BadRequest();
        }

        _logger.LogInformation("Call to DB. Set pass.");

        //update pass to user
        _dbWriter.SetPasswordHash(loginPassword.Key, loginPassword.Value);
        _dbWriter.Update();

        return Ok();
    }

    [Route("addcash")]
    [HttpPost]
    public IActionResult PostCash([FromBody] string login)
    {
        _logger.LogInformation("In PostNewPassword");

        // validate login and check if the user exists
        if (SimpleLoginValidation(login))
        {
            _logger.LogWarning(new InvalidDataException(login), "Invalid input data");

            return BadRequest();
        }

        var user = _dataReader.GetUser(login);

        //check whether need to add funds to the user
        if (user!.Balance < 100)
        {
            _logger.LogInformation("Call to DB. Updating balance to 100 money units.");

            _dbWriter.AddBalance(login, 100 - user.Balance);
            _dbWriter.Update();

            return Ok();
        }
        else
        {
            _logger.LogWarning("Attempt to add more than 100 money unit on balance!");

            return BadRequest();
        }
    }

    private bool SimpleLoginValidation(string login)
    {
        return login is null
            || login.Length == 0
            || login.Length < 6
            || login.Length > 20
            || !_dataReader.IsUserExists(login);
    }
}
