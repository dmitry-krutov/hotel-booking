using Core.Abstractions;
using Core.Mappings;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommand : ICommand, IMapFrom<UpdateHotelRequest>
{
    public Guid Id { get; set; }

    public required string Title { get; init; }

    public required AddressDto Address { get; init; }

    public required string Description { get; init; }

    public HotelId HotelId { get; set; } = null!;

    public Title TitleVo { get; set; } = null!;

    public Address AddressVo { get; set; } = null!;

    public Description DescriptionVo { get; set; } = null!;
}
