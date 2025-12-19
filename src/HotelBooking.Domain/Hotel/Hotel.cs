using CSharpFunctionalExtensions;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using Shared.Errors;
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

    public Title Title { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public Description Description { get; private set; } = null!;

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    public void UpdateDetails(
        Title title,
        Address address,
        Description description)
    {
        Title = title;
        Address = address;
        Description = description;
    }

    public Result<Room, Error> AddRoom(
        Title title,
        PricePerNight pricePerNight,
        Capacity capacity,
        bool isActive)
    {
        return AddRoom(
            RoomId.NewId(),
            title,
            pricePerNight,
            capacity,
            isActive);
    }

    public Result<Room, Error> AddRoom(
        RoomId roomId,
        Title title,
        PricePerNight pricePerNight,
        Capacity capacity,
        bool isActive)
    {
        var room = new Room(
            roomId,
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
            return HotelErrors.Rooms.NotFound(roomId.Value);

        _rooms.Remove(room);
        return Result.Success<Error>();
    }

    public Result<Room, Error> UpdateRoom(
        RoomId roomId,
        Title title,
        PricePerNight pricePerNight,
        Capacity capacity,
        bool isActive)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        if (room is null)
            return HotelErrors.Rooms.NotFound(roomId.Value);

        room.UpdateDetails(title, pricePerNight, capacity, isActive);

        return room;
    }
}
