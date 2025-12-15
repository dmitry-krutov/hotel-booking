namespace HotelBooking.Contracts.Auth;

public sealed record RefreshRequest(
    string RefreshToken);