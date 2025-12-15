using Core.Abstractions;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.DeleteRoom;

public class DeleteRoomCommand : ICommand
{
    public Guid HotelId { get; init; }

    public Guid RoomId { get; init; }

    public HotelId HotelIdVo { get; set; } = null!;

    public RoomId RoomIdVo { get; set; } = null!;
}
