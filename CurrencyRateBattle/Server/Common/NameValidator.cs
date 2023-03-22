using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace Server.Common;

public class NameValidator : AbstractValidator<string>
{
    private readonly Regex _nameRules = new("^[a-zA-Z]*$");

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
            .Must(str => _nameRules.IsMatch(str))
            .WithMessage("Name must contain only latine characters!");
    }

    public override ValidationResult Validate(ValidationContext<string> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure("Name", "User name can not be null!") })
            : base.Validate(context);
    }
}
