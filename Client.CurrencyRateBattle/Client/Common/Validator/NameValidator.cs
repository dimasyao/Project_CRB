using System.Text.RegularExpressions;
using FluentValidation;

namespace Client.Common.Validator;

public class NameValidator : AbstractValidator<string>
{
    private readonly Regex _loginRules = new("^[a-zA-Z]*$");

    public NameValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        _ = RuleFor(p => p)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name field is empty!")
            .MinimumLength(2)
            .WithMessage("Name length must be more than six characters!")
            .MaximumLength(30)
            .WithMessage("Name length must be less than twenty characters!")
            .Must(str => _loginRules.IsMatch(str))
            .WithMessage("Name must contain only latine characters!");
    }
}
