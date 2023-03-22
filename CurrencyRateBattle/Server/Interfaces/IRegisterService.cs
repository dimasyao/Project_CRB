using FluentValidation.Results;
using SharedModels.Models;

namespace Server.Interfaces;

public interface IRegisterService
{
    ValidationResult WriteNewUserAsync(User user);
}
