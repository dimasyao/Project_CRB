using System.Text.RegularExpressions;
using FluentValidation;

namespace Client.Common.Validator;

public class LoginValidator : AbstractValidator<string>
{
    private readonly Regex _loginRules = new("^[a-zA-Z0-9-_@.]*$");

    public LoginValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        _ = RuleFor(p => p)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Login field is empty!")
            .MinimumLength(6)
            .WithMessage("Login length must be more than six characters!")
            .MaximumLength(30)
            .WithMessage("Login length must be less than twenty characters!")
            .Must(str => _loginRules.IsMatch(str))
            .WithMessage("Login must contain only latine characters (accept numbers and '_', '-', '@', '.' symbols)!")
            .EmailAddress()
            .WithMessage("Login should be email!");
    }
}
