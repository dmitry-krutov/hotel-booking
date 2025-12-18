namespace HotelBooking.Contracts.Hotels;

public sealed class HotelSearchResultDto
{
    public Guid HotelId { get; init; }

    public required string Title { get; init; }

    public required string City { get; init; }

    public required string Country { get; init; }

    public decimal MinPricePerNight { get; init; }

    public int AvailableRoomsCount { get; init; }
}
