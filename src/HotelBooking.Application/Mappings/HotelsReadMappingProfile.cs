using AutoMapper;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Contracts.Hotels;

namespace HotelBooking.Application.Mappings;

public sealed class HotelsReadMappingProfile : Profile
{
    public HotelsReadMappingProfile()
    {
        CreateMap<HotelReadModel, AddressDto>()
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
            .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Region))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Street))
            .ForMember(d => d.Building, opt => opt.MapFrom(s => s.Building))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.PostalCode));

        CreateMap<HotelReadModel, HotelDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s));

        CreateMap<HotelReadModel, HotelDetailsDto>()
            .IncludeBase<HotelReadModel, HotelDto>()
            .ForMember(d => d.Rooms, opt => opt.MapFrom(s => s.Rooms));

        CreateMap<RoomReadModel, RoomDto>();

        CreateMap<HotelSearchResultReadModel, HotelSearchResultDto>();
    }
}