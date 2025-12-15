using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.EnableTwoFactor;

public sealed class EnableTwoFactorCommand : ICommand
{
    public required Guid UserId { get; init; }

    public required string Code { get; init; }
}