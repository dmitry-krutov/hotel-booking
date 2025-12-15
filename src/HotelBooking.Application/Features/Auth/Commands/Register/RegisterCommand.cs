using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.Register;

public class RegisterCommand : ICommand
{
    public required string UserName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required string ConfirmPassword { get; init; }
}