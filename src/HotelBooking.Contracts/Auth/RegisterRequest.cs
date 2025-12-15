namespace HotelBooking.Contracts.Auth;

public sealed record RegisterRequest(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword);