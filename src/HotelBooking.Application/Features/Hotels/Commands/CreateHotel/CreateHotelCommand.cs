using Core.Abstractions;
using Core.Mappings;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel.ValueObjects;

namespace HotelBooking.Application.Features.Hotels.Commands.CreateHotel;

public class CreateHotelCommand : ICommand, IMapFrom<CreateHotelRequest>
{
    public required string Title { get; init; }

    public required AddressDto Address { get; init; }

    public required string Description { get; init; }

    public Title TitleVo { get; set; } = null!;

    public Address AddressVo { get; set; } = null!;

    public Description DescriptionVo { get; set; } = null!;
}