using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.Refresh;

public sealed class RefreshCommandHandler : ICommandHandler<TokenResponse, RefreshCommand>
{
    private readonly IAuthService _authService;
    private readonly IValidator<RefreshCommand> _validator;

    public RefreshCommandHandler(IAuthService authService, IValidator<RefreshCommand> validator)
    {
        _authService = authService;
        _validator = validator;
    }

    public async Task<Result<TokenResponse, ErrorList>> Handle(
        RefreshCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var result = await _authService.RefreshAsync(command.RefreshToken, cancellationToken);
        return result.ToErrorList();
    }
}