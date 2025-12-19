using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.GenerateTwoFactorSecret;

public sealed class GenerateTwoFactorSecretCommandValidator : AbstractValidator<GenerateTwoFactorSecretCommand>
{
    public GenerateTwoFactorSecretCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(AuthErrors.Validation.UserIdRequired());
    }
}
