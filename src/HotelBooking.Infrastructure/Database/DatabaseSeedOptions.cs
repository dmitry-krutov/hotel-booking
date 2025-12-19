namespace HotelBooking.Infrastructure.Database;

public class DatabaseSeedOptions
{
    public const string SECTION_NAME = "DatabaseSeed";

    public bool EnableSeeding { get; set; } = false;

    public bool ClearDatabase { get; set; } = false;
}
