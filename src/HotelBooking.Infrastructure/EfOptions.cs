namespace HotelBooking.Infrastructure;

public class EfOptions
{
    public const string SECTION_NAME = "Ef";

    public bool EnableSensitiveLogging { get; set; } = false;
}