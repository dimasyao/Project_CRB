using DbProvider.Interfaces;
using Server.Interfaces;
using SharedModels.Models;

namespace Server.Services;

public class SignInService : ISignInService
{
    private readonly ILogger<SignInService> _logger;
    private readonly IDataReader _dataReader;

    public SignInService(ILogger<SignInService> logger, IDataReader dataReader)
    {
        _logger = logger;
        _dataReader = dataReader;
    }

    public bool IsValidLoginPass(User user)
    {
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}: In IsValidLoginPass {user?.ToString()}");

        return user != null
            && user.Login != null
            && user.Password != null
            && _dataReader.IsUserExists(user.Login)
            && user.Password == _dataReader.GetPasswordHash(user.Login);
    }
}
