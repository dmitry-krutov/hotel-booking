using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<TokenResponse, RegisterCommand>
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterCommandHandler(
        IAuthService authService,
        IValidator<RegisterCommand> validator)
    {
        _authService = authService;
        _validator = validator;
    }

    public async Task<Result<TokenResponse, ErrorList>> Handle(
        RegisterCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var result = await _authService.RegisterAsync(
            command.UserName,
            command.Email,
            command.Password,
            command.ConfirmPassword,
            cancellationToken);
        return result.ToErrorList();
    }
}