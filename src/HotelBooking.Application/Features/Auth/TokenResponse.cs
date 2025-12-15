namespace HotelBooking.Application.Features.Auth;

public record TokenResponse(string AccessToken, string RefreshToken);