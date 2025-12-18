using Core.Abstractions;

namespace HotelBooking.Application.Features.Bookings.Queries.GetBookingById;

public sealed class GetBookingByIdQuery : IQuery
{
    public Guid Id { get; set; }

    public Guid RequesterId { get; set; }
}
