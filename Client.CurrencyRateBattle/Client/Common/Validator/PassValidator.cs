using System.Text.RegularExpressions;
using FluentValidation;

namespace Client.Common.Validator;

public class PassValidator : AbstractValidator<string>
{
    private readonly Regex _passwordRules = new("^[a-zA-Z0-9!@#$]*$");

    public PassValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        _ = RuleFor(p => p)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password length must be more than six characters!")
            .MaximumLength(20)
            .WithMessage("Password length must be more than six characters!")
            .Must(str => _passwordRules.IsMatch(str))
            .WithMessage("Password must contain one or more symbol!")
            .Must(str => str.Any(chr => char.IsUpper(chr)))
            .WithMessage("Password must contain one or more upper character ")
            .Must(str => str.Any(chr => char.IsDigit(chr)))
            .WithMessage("Password must contain one or more number")
            .Must(str => str.Any(chr => char.IsAscii(chr)))
            .WithMessage("Password must contain only latine characters, numbers or symbols");
    }
}
