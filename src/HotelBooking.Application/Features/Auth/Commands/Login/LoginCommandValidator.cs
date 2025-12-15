using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(Error.Validation("userName.empty", "UserName is required").Serialize())
            .MaximumLength(50)
            .WithMessage(Error.Validation("userName.maxLength", "UserName must be at most 50 characters").Serialize());

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(Error.Validation("password.empty", "Password is required").Serialize())
            .MaximumLength(200)
            .WithMessage(Error.Validation("password.maxLength", "Password is too long").Serialize());

        RuleFor(x => x.TwoFactorCode)
            .MaximumLength(20)
            .WithMessage(Error.Validation("twoFactorCode.maxLength", "Two-factor code is too long").Serialize())
            .When(x => !string.IsNullOrWhiteSpace(x.TwoFactorCode));
    }
}