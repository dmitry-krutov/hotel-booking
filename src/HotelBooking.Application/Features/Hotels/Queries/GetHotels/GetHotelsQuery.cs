using Core.Abstractions;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotels;

public sealed class GetHotelsQuery : IQuery
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
