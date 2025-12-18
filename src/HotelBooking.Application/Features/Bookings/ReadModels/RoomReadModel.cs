namespace HotelBooking.Application.Features.Bookings.ReadModels;

public class RoomReadModel
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public required string Title { get; set; }

    public decimal PricePerNight { get; set; }

    public int Capacity { get; set; }

    public bool IsActive { get; set; }

    public HotelReadModel Hotel { get; set; } = null!;
}
