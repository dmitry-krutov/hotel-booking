namespace HotelBooking.Contracts.Auth;

public sealed record LoginRequest(
    string UserName,
    string Password,
    string? TwoFactorCode);