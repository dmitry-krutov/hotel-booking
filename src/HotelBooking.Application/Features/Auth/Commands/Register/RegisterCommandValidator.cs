using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(Error.Validation("userName.empty", "UserName is required").Serialize())
            .MaximumLength(50)
            .WithMessage(Error.Validation("userName.maxLength", "UserName must be at most 50 characters").Serialize());

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(Error.Validation("email.empty", "Email is required").Serialize())
            .EmailAddress()
            .WithMessage(Error.Validation("email.invalid", "Email format is invalid").Serialize())
            .MaximumLength(256)
            .WithMessage(Error.Validation("email.maxLength", "Email must be at most 256 characters").Serialize());

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(Error.Validation("password.empty", "Password is required").Serialize())
            .MinimumLength(6)
            .WithMessage(Error.Validation("password.minLength", "Password must be at least 6 characters").Serialize());

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage(Error.Validation("confirmPassword.empty", "ConfirmPassword is required").Serialize());

        RuleFor(x => x)
            .Must(x => string.Equals(x.Password, x.ConfirmPassword, StringComparison.Ordinal))
            .WithMessage(Error.Validation("password.mismatch", "Passwords do not match").Serialize());
    }
}