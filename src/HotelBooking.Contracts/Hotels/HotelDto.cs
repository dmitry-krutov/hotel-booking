namespace HotelBooking.Contracts.Hotels;

public class HotelDto
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required AddressDto Address { get; init; }

    public required string Description { get; init; }
}