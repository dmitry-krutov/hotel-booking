using CSharpFunctionalExtensions;
using HotelBooking.Domain.Booking;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings;

public interface IBookingRepository
{
    Task<Result<Guid, Error>> Add(Booking booking, CancellationToken cancellationToken);

    Task<bool> IsRoomAvailable(RoomId roomId, DateRange period, CancellationToken cancellationToken);
}
