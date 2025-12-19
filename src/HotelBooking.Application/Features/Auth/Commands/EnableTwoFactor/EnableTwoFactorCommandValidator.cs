using System.Text.RegularExpressions;
using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.EnableTwoFactor;

public sealed class EnableTwoFactorCommandValidator : AbstractValidator<EnableTwoFactorCommand>
{
    private static readonly Regex SixDigitsRegex = new(@"^\d{6}$", RegexOptions.Compiled);

    public EnableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(AuthErrors.Validation.UserIdRequired());

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithError(AuthErrors.Validation.TwoFactorCodeRequired())
            .Must(code => SixDigitsRegex.IsMatch(code))
            .WithError(AuthErrors.Validation.TwoFactorCodeInvalidLength());
    }
}
