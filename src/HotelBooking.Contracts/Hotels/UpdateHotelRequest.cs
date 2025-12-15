namespace HotelBooking.Contracts.Hotels;

public class UpdateHotelRequest
{
    public required string Title { get; init; }

    public required AddressDto Address { get; init; }

    public required string Description { get; init; }
}
