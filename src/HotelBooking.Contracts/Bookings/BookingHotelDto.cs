namespace HotelBooking.Contracts.Bookings;

public class BookingHotelDto
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required string Country { get; init; }

    public required string City { get; init; }

    public string? Region { get; init; }

    public required string Street { get; init; }

    public string? Building { get; init; }

    public string? PostalCode { get; init; }

    public required string Description { get; init; }
}
