using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IAuthService _authService;
    private readonly IValidator<LogoutCommand> _validator;

    public LogoutCommandHandler(IAuthService authService, IValidator<LogoutCommand> validator)
    {
        _authService = authService;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return UnitResult.Failure(validationResult.ToList());

        var result = await _authService.LogoutAsync(command.RefreshToken, cancellationToken);

        return result.ToErrorList();
    }
}