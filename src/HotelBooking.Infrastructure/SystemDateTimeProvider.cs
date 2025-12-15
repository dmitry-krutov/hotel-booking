using Core.Abstractions;

namespace HotelBooking.Infrastructure;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}