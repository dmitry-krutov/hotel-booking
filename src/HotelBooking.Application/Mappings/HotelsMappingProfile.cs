using AutoMapper;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.Hotel.ValueObjects;

namespace HotelBooking.Application.Mappings;

public sealed class HotelsMappingProfile : Profile
{
    public HotelsMappingProfile()
    {
        CreateMap<Address, AddressDto>();

        CreateMap<Hotel, HotelDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title.Value))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.Value))
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address));

        CreateMap<Room, RoomDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.HotelId, opt => opt.MapFrom(s => s.HotelId.Value))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title.Value))
            .ForMember(d => d.PricePerNight, opt => opt.MapFrom(s => s.PricePerNight.Value))
            .ForMember(d => d.Capacity, opt => opt.MapFrom(s => s.Capacity.Value));
    }
}