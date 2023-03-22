namespace Client.Interfaces;

public interface IPersonalOfficeHandler
{
    /// <summary>
    /// Tries to get personal statistics as total played games, victories etc., from server
    /// </summary>
    void ViewInformation(string login);
    /// <summary>
    /// Tries to change name of user in database
    /// </summary>
    void ChangeName(string login);
    /// <summary>
    /// Tries to change password of user in database
    /// </summary>
    void ChangePassword(string login);
    /// <summary>
    /// Tries to change user balance (adds 100) in database
    /// </summary>
    void AddCash(string login);
}
