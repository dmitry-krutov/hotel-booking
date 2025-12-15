using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.Login;

public sealed class LoginCommand : ICommand
{
    public required string UserName { get; init; }

    public required string Password { get; init; }

    public string? TwoFactorCode { get; init; }
}