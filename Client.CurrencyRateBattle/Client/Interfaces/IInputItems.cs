using Client.Common.CodeWays;
using Client.Models;
using FluentValidation.Results;

namespace Client.Interfaces;

public interface IInputItems
{
    /// <summary>
    /// Universal procesing of input user items
    /// </summary>
    /// <param name="userItem">some user items</param>
    /// <returns>result of validation and input value if value valid</returns>
    (ValidationResult, string) InputData(UserItem userItem);

    /// <summary>
    /// Universal procesing of http requesting
    /// </summary>
    /// <param name="httpWay">http way from enum</param>
    /// <param name="user">input user</param>
    /// <returns>result of hhtp request</returns>
    bool SendRequest(HttpWay httpWay, User user);
}
