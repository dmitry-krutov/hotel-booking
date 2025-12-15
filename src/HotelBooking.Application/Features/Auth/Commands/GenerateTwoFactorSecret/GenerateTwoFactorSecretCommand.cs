using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.GenerateTwoFactorSecret;

public sealed class GenerateTwoFactorSecretCommand : ICommand
{
    public required Guid UserId { get; init; }
}