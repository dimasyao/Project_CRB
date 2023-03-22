using System.Text.RegularExpressions;
using DbProvider.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using SharedModels.Models;

namespace Server.Common;

public class BetValidator : AbstractValidator<Bet>
{
    private readonly Regex _nameRules = new("^[A-Z]*$");

    public BetValidator(IDataReader dbReader)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        _ = RuleFor(b => b.Login)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Login is empty")
            .Must(l => dbReader.IsUserExists(l!))
            .WithMessage("Login is invalid");

        _ = RuleFor(b => b.Currency)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Empty currancy value!")
            .MaximumLength(3)
            .MinimumLength(3)
            .WithMessage("Invalid currancy length")
            .Must(curr => _nameRules.IsMatch(curr))
            .WithMessage("Invalid currancy name.");

        _ = RuleFor(b => b.Sum)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Summ value is empty!")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Summ should be great than 0!");

        _ = RuleFor(b => b.BetValue)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Bet value is empty!")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Bet value should be great than 0!");

        _ = RuleFor(b => b)
            .Must(b => dbReader.GetUser(b.Login!)!.Balance >= b.Sum)
            .WithMessage("Sorry, you do not have enough money!");
    }

    public override ValidationResult Validate(ValidationContext<Bet> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure("Bet", "Bet can not be null!") })
            : base.Validate(context);
    }
}
