namespace HotelBooking.Contracts.Hotels;

public sealed class AddressDto
{
    public required string Country { get; init; }

    public required string City { get; init; }

    public string? Region { get; init; }

    public required string Street { get; init; }

    public string? Building { get; init; }

    public string? PostalCode { get; init; }
}