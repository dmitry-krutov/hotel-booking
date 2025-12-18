using HotelBooking.Application.Features.Hotels.ReadModels;

namespace HotelBooking.Application.Features.Bookings.ReadModels;

public class BookingReadModel
{
    public Guid Id { get; set; }

    public Guid HotelId { get; set; }

    public Guid RoomId { get; set; }

    public Guid UserId { get; set; }

    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }

    public int Guests { get; set; }

    public decimal TotalPrice { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public HotelReadModel Hotel { get; set; } = null!;

    public RoomReadModel Room { get; set; } = null!;
}
