using Core.Abstractions;

namespace HotelBooking.Application.Features.Bookings.Queries.GetUserBookings;

public class GetUserBookingsQuery : IQuery
{
    public Guid UserId { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
