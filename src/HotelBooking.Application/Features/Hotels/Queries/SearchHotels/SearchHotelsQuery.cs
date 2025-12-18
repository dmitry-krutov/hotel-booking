using Core.Abstractions;
using HotelBooking.Contracts.Hotels;

namespace HotelBooking.Application.Features.Hotels.Queries.SearchHotels;

public sealed class SearchHotelsQuery : IQuery
{
    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }

    public string? City { get; init; }

    public string? Country { get; init; }

    public string? Region { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public HotelSearchSortOption Sort { get; init; }
}
