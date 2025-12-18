using HotelBooking.Application.Features.Bookings.ReadModels;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings;

public interface IBookingReadRepository
{
    Task<PagedList<BookingReadModel>> GetUserBookings(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
