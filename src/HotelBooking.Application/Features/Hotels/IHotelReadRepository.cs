using HotelBooking.Application.Features.Hotels.ReadModels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels;

public interface IHotelReadRepository
{
    Task<PagedList<HotelReadModel>> GetHotels(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<HotelReadModel?> GetById(Guid hotelId, CancellationToken cancellationToken);
}
