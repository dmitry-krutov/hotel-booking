using Core.Abstractions;

namespace HotelBooking.Application.Features.Auth.Commands.Refresh;

public class RefreshCommand : ICommand
{
    public required string RefreshToken { get; init; }
}