namespace HotelBooking.Contracts.Hotels;

public class GetHotelsRequest
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
