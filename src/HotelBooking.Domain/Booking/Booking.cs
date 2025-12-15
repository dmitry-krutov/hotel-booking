using HotelBooking.Domain.Booking.Enums;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Domain.Booking;

public sealed class Booking : DomainEntity<BookingId>
{
    public Booking(
        BookingId id,
        HotelId hotelId,
        RoomId roomId,
        Guid userId,
        DateRange period,
        GuestsCount guests)
        : base(id)
    {
        HotelId = hotelId;
        RoomId = roomId;
        UserId = userId;
        Period = period;
        Guests = guests;
        Status = BookingStatus.ACTIVE;
        CreatedAtUtc = DateTime.UtcNow;
    }

    private Booking(BookingId id)
        : base(id)
    {
    }

    public HotelId HotelId { get; private set; }

    public RoomId RoomId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange Period { get; private set; }

    public GuestsCount Guests { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
}