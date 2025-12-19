using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.DisableTwoFactor;

public sealed class DisableTwoFactorCommandValidator : AbstractValidator<DisableTwoFactorCommand>
{
    public DisableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(AuthErrors.Validation.UserIdRequired());
    }
}
