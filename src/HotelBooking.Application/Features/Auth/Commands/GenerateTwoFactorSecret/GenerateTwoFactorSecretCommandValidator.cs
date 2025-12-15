using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.GenerateTwoFactorSecret;

public sealed class GenerateTwoFactorSecretCommandValidator : AbstractValidator<GenerateTwoFactorSecretCommand>
{
    public GenerateTwoFactorSecretCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(Error.Validation("userId.empty", "UserId is required").Serialize());
    }
}