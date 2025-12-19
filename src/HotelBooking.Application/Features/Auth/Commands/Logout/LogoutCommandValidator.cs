using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithError(AuthErrors.Validation.RefreshTokenRequired())
            .MaximumLength(2048)
            .WithError(AuthErrors.Validation.RefreshTokenTooLong());
    }
}
