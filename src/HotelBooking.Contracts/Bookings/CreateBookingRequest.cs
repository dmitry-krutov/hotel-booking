namespace HotelBooking.Contracts.Bookings;

public class CreateBookingRequest
{
    public Guid HotelId { get; init; }

    public Guid RoomId { get; init; }

    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }
}
