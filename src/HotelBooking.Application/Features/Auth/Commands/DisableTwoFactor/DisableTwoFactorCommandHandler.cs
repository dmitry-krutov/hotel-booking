using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.DisableTwoFactor;

public sealed class DisableTwoFactorCommandHandler
    : ICommandHandler<Guid, DisableTwoFactorCommand>
{
    private readonly ITwoFactorService _twoFactorService;
    private readonly IValidator<DisableTwoFactorCommand> _validator;

    public DisableTwoFactorCommandHandler(
        ITwoFactorService twoFactorService,
        IValidator<DisableTwoFactorCommand> validator)
    {
        _twoFactorService = twoFactorService;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DisableTwoFactorCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        return await _twoFactorService.DisableTwoFactorAsync(command.UserId, cancellationToken);
    }
}