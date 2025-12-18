using AutoMapper;
using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Contracts.Bookings;

namespace HotelBooking.Application.Mappings;

public sealed class BookingsReadMappingProfile : Profile
{
    public BookingsReadMappingProfile()
    {
        CreateMap<BookingReadModel, UserBookingDto>();
        CreateMap<HotelReadModel, BookingHotelDto>();
        CreateMap<RoomReadModel, BookingRoomDto>();
    }
}
