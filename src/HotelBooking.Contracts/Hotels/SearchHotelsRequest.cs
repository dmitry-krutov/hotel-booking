namespace HotelBooking.Contracts.Hotels;

public sealed class SearchHotelsRequest
{
    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }

    public int Guests { get; init; }

    public string? City { get; init; }

    public string? Country { get; init; }

    public string? Region { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public HotelSearchSortOption Sort { get; init; } = HotelSearchSortOption.PriceAsc;
}
