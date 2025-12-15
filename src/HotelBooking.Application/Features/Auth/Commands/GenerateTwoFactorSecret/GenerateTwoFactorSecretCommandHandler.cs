using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth.Commands.GenerateTwoFactorSecret;

public sealed class GenerateTwoFactorSecretCommandHandler
    : ICommandHandler<string, GenerateTwoFactorSecretCommand>
{
    private readonly ITwoFactorService _twoFactorService;
    private readonly IValidator<GenerateTwoFactorSecretCommand> _validator;

    public GenerateTwoFactorSecretCommandHandler(
        ITwoFactorService twoFactorService,
        IValidator<GenerateTwoFactorSecretCommand> validator)
    {
        _twoFactorService = twoFactorService;
        _validator = validator;
    }

    public async Task<Result<string, ErrorList>> Handle(
        GenerateTwoFactorSecretCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        return await _twoFactorService.GenerateTwoFactorSecretAsync(command.UserId, cancellationToken);
    }
}