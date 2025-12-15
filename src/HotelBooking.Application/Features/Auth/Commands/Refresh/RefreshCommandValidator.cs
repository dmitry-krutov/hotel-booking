using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Refresh;

public sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(Error.Validation("refreshToken.empty", "RefreshToken is required").Serialize())
            .MaximumLength(2048)
            .WithMessage(Error.Validation("refreshToken.maxLength", "RefreshToken is too long").Serialize());
    }
}