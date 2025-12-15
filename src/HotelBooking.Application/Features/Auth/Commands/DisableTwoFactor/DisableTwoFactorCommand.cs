using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.DisableTwoFactor;

public sealed class DisableTwoFactorCommand : ICommand
{
    public required Guid UserId { get; init; }
}