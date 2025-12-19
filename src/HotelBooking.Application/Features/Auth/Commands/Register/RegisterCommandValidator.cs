using Core.Validation;
using FluentValidation;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithError(AuthErrors.Validation.UserNameRequired())
            .MaximumLength(50)
            .WithError(AuthErrors.Validation.UserNameTooLong());

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithError(AuthErrors.Validation.EmailRequired())
            .EmailAddress()
            .WithError(AuthErrors.Validation.EmailInvalid())
            .MaximumLength(256)
            .WithError(AuthErrors.Validation.EmailTooLong());

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithError(AuthErrors.Validation.PasswordRequired())
            .MinimumLength(6)
            .WithError(AuthErrors.Validation.PasswordTooShort());

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithError(AuthErrors.Validation.ConfirmPasswordRequired());

        RuleFor(x => x)
            .Custom((command, context) =>
            {
                if (!string.Equals(command.Password, command.ConfirmPassword, StringComparison.Ordinal))
                {
                    context.AddFailure(nameof(command.ConfirmPassword), AuthErrors.Identity.PasswordsDoNotMatch().Serialize());
                }
            });
    }
}
