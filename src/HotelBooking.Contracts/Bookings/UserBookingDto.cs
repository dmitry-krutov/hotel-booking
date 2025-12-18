namespace HotelBooking.Contracts.Bookings;

public class UserBookingDto
{
    public Guid Id { get; init; }

    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }

    public decimal TotalPrice { get; init; }

    public int Status { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public required BookingHotelDto Hotel { get; set; }

    public required BookingRoomDto Room { get; set; }
}
