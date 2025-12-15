using Core.Abstractions;
using Core.Mappings;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.AddRoom;

public class AddRoomCommand : ICommand, IMapFrom<AddRoomRequest>
{
    public Guid HotelId { get; init; }

    public required string Title { get; init; }

    public decimal PricePerNight { get; init; }

    public int Capacity { get; init; }

    public bool IsActive { get; init; }

    public HotelId HotelIdVo { get; set; } = null!;

    public Title TitleVo { get; set; } = null!;

    public PricePerNight PricePerNightVo { get; set; } = null!;

    public Capacity CapacityVo { get; set; } = null!;
}