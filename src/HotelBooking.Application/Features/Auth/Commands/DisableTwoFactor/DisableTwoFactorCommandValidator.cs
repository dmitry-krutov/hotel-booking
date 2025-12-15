using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.DisableTwoFactor;

public sealed class DisableTwoFactorCommandValidator : AbstractValidator<DisableTwoFactorCommand>
{
    public DisableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(Error.Validation("userId.empty", "UserId is required").Serialize());
    }
}