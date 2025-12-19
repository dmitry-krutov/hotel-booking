using HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;
using HotelBooking.Application.Features.Hotels.ReadModels;

namespace HotelBooking.Application.Features.Hotels;

public interface IHotelAvailabilityReadRepository
{
    Task<HotelReadModel?> GetHotelWithAvailableRooms(
        GetHotelAvailabilityQuery query,
        CancellationToken cancellationToken);
}
