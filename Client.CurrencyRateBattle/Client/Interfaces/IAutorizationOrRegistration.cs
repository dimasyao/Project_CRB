using Client.Models;

namespace Client.Interfaces;

public interface IAutorizationOrRegistration
{
    /// <summary>
    /// Checks user existance and validity of login and password
    /// </summary>
    /// <returns>logged in user instance</returns>
    public User Autorization();

    /// <summary>
    /// Tries to register new user in database
    /// </summary>
    /// <returns>registered and logged in user instance</returns>
    public User Registration();
}
