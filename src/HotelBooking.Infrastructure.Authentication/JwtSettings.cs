namespace HotelBooking.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SECTION_NAME = "Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string RefreshHashKey { get; set; } = string.Empty;

    public int AccessTokenExpirationMinutes { get; set; }

    public int RefreshTokenExpirationDays { get; set; }

    public int RefreshTokenAbsoluteExpirationDays { get; set; }
}