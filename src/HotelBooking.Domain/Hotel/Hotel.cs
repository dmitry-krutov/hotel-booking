using CSharpFunctionalExtensions;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Domain.Hotel;

public class Hotel : DomainEntity<HotelId>
{
    private readonly List<Room> _rooms = new();

    public Hotel(
        HotelId id,
        Title title,
        Address address,
        Description description)
        : base(id)
    {
        Title = title;
        Address = address;
        Description = description;
    }

    private Hotel(HotelId id)
        : base(id)
    {
    }

    public Title Title { get; private set; }

    public Address Address { get; private set; } = null!;

    public Description Description { get; private set; }

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    public Room AddRoom(
        Title title,
        PricePerNight pricePerNight,
        Capacity capacity,
        bool isActive)
    {
        var room = new Room(
            RoomId.NewId(),
            Id,
            title,
            pricePerNight,
            capacity,
            isActive);

        _rooms.Add(room);

        return room;
    }


    public UnitResult<Error> RemoveRoom(RoomId roomId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        if (room is null)
            return GeneralErrors.Entity.NotFound(nameof(Room));

        _rooms.Remove(room);
        return Result.Success<Error>();
    }
}