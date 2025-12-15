namespace HotelBooking.Infrastructure.Authentication;

public static class RefreshRevokeReasons
{
    public const string ROTATED = "rotated";
    public const string LOGOUT = "logout";
    public const string REUSE_DETECTED = "reuse-detected";
}