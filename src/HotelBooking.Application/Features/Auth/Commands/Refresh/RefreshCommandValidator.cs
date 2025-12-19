using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Refresh;

public sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithError(AuthErrors.Validation.RefreshTokenRequired())
            .MaximumLength(2048)
            .WithError(AuthErrors.Validation.RefreshTokenTooLong());
    }
}
