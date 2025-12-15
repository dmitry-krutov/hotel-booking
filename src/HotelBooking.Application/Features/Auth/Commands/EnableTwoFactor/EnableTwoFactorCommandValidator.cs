using System.Text.RegularExpressions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.EnableTwoFactor;

public sealed class EnableTwoFactorCommandValidator : AbstractValidator<EnableTwoFactorCommand>
{
    private static readonly Regex SixDigitsRegex = new(@"^\d{6}$", RegexOptions.Compiled);

    public EnableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(Error.Validation("userId.empty", "UserId is required").Serialize());

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(Error.Validation("twoFactor.code.empty", "Two-factor code is required").Serialize())
            .Must(code => SixDigitsRegex.IsMatch(code))
            .WithMessage(Error.Validation("twoFactor.code.invalid", "Two-factor code must be exactly 6 digits")
                .Serialize());
    }
}