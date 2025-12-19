using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithError(AuthErrors.Validation.UserNameRequired())
            .MaximumLength(50)
            .WithError(AuthErrors.Validation.UserNameTooLong());

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithError(AuthErrors.Validation.PasswordRequired())
            .MaximumLength(200)
            .WithError(AuthErrors.Validation.PasswordTooLong());

        RuleFor(x => x.TwoFactorCode)
            .MaximumLength(20)
            .WithError(AuthErrors.Validation.TwoFactorCodeTooLong())
            .When(x => !string.IsNullOrWhiteSpace(x.TwoFactorCode));
    }
}
