namespace HotelBooking.Contracts.Bookings;

public class GetUserBookingsRequest
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
