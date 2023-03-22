using FluentValidation;
using System.Text.RegularExpressions;
using SharedModels.Models;
using FluentValidation.Results;
using DbProvider.Interfaces;

namespace Server.Common;

public class UserRegistryValidator : AbstractValidator<User>
{
    private readonly Regex _loginRules = new("^[a-zA-Z0-9-_@.]*$");
    private readonly Regex _nameRules = new("^[a-zA-Z]*$");

    public UserRegistryValidator(IDataReader dataReader)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        _ = RuleFor(p => p.Login)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Login field is empty!")
            .MinimumLength(6)
            .WithMessage("Login length must be more than six characters!")
            .MaximumLength(20)
            .WithMessage("Login length must be less than twenty characters!")
            .Must(str => _loginRules.IsMatch(str))
            .WithMessage("Login must contain only latine characters (accept numbers and '_', '-', '@', '.' symbols)!")
            .EmailAddress()
            .WithMessage("Login should be email!")
            .Must(str => !dataReader.IsUserExists(str))
            .WithMessage("Login is busy!");

        _ = RuleFor(p => p.Password)
           .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password length must be more than six characters!");

        _ = RuleFor(p => p.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name field is empty!")
            .MinimumLength(2)
            .WithMessage("Name length must be more than six characters!")
            .MaximumLength(30)
            .WithMessage("Name length must be less than twenty characters!")
            .Must(str => _nameRules.IsMatch(str))
            .WithMessage("Name must contain only latine characters!");
    }

    public override ValidationResult Validate(ValidationContext<User> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure("User", "User can not be null!") })
            : base.Validate(context);
    }
}
