using SharedModels.Models;

namespace Server.Interfaces;

public interface ISignInService
{
    /// <summary>
    /// Validate user login and pass
    /// </summary>
    /// <param name="user"></param>
    /// <returns>bool result</returns>
    bool IsValidLoginPass(User user);
}
