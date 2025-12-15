namespace HotelBooking.Contracts.Hotels;

public class RoomDto
{
    public Guid Id { get; init; }

    public Guid HotelId { get; init; }

    public required string Title { get; init; }

    public decimal PricePerNight { get; init; }

    public int Capacity { get; init; }

    public bool IsActive { get; init; }
}
