using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Domain.Hotel;

public class Room : DomainEntity<RoomId>
{
    public Room(
        RoomId id,
        HotelId hotelId,
        Title title,
        PricePerNight pricePerNight,
        Capacity capacity,
        bool isActive)
        : base(id)
    {
        HotelId = hotelId;
        Title = title;
        PricePerNight = pricePerNight;
        Capacity = capacity;
        IsActive = isActive;
    }

    private Room(RoomId id)
        : base(id)
    {
    }

    public HotelId HotelId { get; private set; } = null!;

    public Title Title { get; private set; } = null!;

    public PricePerNight PricePerNight { get; private set; } = null!;

    public Capacity Capacity { get; private set; } = null!;

    public bool IsActive { get; private set; }
}