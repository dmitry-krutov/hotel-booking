using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler : ICommandHandler<TokenResponse, LoginCommand>
{
    private readonly IAuthService _authService;
    private readonly IValidator<LoginCommand> _validator;

    public LoginCommandHandler(IAuthService authService, IValidator<LoginCommand> validator)
    {
        _authService = authService;
        _validator = validator;
    }

    public async Task<Result<TokenResponse, ErrorList>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var result = await _authService.LoginAsync(
            command.UserName,
            command.Password,
            command.TwoFactorCode,
            cancellationToken);

        return result.ToErrorList();
    }
}