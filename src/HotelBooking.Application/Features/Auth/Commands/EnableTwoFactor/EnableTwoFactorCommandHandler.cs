using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.EnableTwoFactor;

public sealed class EnableTwoFactorCommandHandler
    : ICommandHandler<Guid, EnableTwoFactorCommand>
{
    private readonly ITwoFactorService _twoFactorService;
    private readonly IValidator<EnableTwoFactorCommand> _validator;

    public EnableTwoFactorCommandHandler(
        ITwoFactorService twoFactorService,
        IValidator<EnableTwoFactorCommand> validator)
    {
        _twoFactorService = twoFactorService;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        EnableTwoFactorCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        return await _twoFactorService.EnableTwoFactorAsync(
            command.UserId,
            command.Code,
            cancellationToken);
    }
}