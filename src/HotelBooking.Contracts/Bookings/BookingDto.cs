namespace HotelBooking.Contracts.Bookings;

public class BookingDto
{
    public Guid Id { get; init; }

    public Guid HotelId { get; init; }

    public Guid RoomId { get; init; }

    public Guid UserId { get; init; }

    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }

    public decimal TotalPrice { get; init; }

    public int Status { get; init; }

    public DateTime CreatedAtUtc { get; init; }
}
