namespace HotelBooking.Contracts.Auth;

public sealed record EnableTwoFactorRequest(
    string Code);