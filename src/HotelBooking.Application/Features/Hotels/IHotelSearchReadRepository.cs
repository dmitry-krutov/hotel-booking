using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Application.Features.Hotels.Queries.SearchHotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels;

public interface IHotelSearchReadRepository
{
    Task<PagedList<HotelSearchResultReadModel>> SearchHotels(
        SearchHotelsQuery query,
        CancellationToken cancellationToken);
}
