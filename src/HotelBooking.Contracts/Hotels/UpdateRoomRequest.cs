namespace HotelBooking.Contracts.Hotels;

public class UpdateRoomRequest
{
    public required string Title { get; init; }

    public decimal PricePerNight { get; init; }

    public int Capacity { get; init; }

    public bool IsActive { get; init; }
}
