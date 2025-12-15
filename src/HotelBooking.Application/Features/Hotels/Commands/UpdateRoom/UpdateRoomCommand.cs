using Core.Abstractions;
using Core.Mappings;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.UpdateRoom;

public class UpdateRoomCommand : ICommand, IMapFrom<UpdateRoomRequest>
{
    public Guid HotelId { get; set; }

    public Guid RoomId { get; set; }

    public required string Title { get; init; }

    public decimal PricePerNight { get; init; }

    public int Capacity { get; init; }

    public bool IsActive { get; init; }

    public HotelId HotelIdVo { get; set; } = null!;

    public RoomId RoomIdVo { get; set; } = null!;

    public Title TitleVo { get; set; } = null!;

    public PricePerNight PricePerNightVo { get; set; } = null!;

    public Capacity CapacityVo { get; set; } = null!;
}
