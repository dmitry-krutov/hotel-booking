using AutoMapper;
using HotelBooking.Contracts.Bookings;
using HotelBooking.Domain.Booking;

namespace HotelBooking.Application.Mappings;

public sealed class BookingsMappingProfile : Profile
{
    public BookingsMappingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.HotelId, opt => opt.MapFrom(s => s.HotelId.Value))
            .ForMember(d => d.RoomId, opt => opt.MapFrom(s => s.RoomId.Value))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.CheckIn, opt => opt.MapFrom(s => s.Period.CheckIn))
            .ForMember(d => d.CheckOut, opt => opt.MapFrom(s => s.Period.CheckOut))
            .ForMember(d => d.Guests, opt => opt.MapFrom(s => s.Guests.Value))
            .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s => s.TotalPrice))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => (int)s.Status))
            .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc));
    }
}
