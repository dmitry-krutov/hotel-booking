using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommand : ICommand
{
    public required string RefreshToken { get; init; }
}