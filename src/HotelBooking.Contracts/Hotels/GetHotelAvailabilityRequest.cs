namespace HotelBooking.Contracts.Hotels;

public sealed class GetHotelAvailabilityRequest
{
    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }
}
