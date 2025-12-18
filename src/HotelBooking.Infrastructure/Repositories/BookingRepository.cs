using CSharpFunctionalExtensions;
using HotelBooking.Application.Features.Bookings;
using HotelBooking.Domain.Booking;
using HotelBooking.Domain.Booking.Enums;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace HotelBooking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationWriteDbContext _context;

    public BookingRepository(ApplicationWriteDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid, Error>> Add(Booking booking, CancellationToken cancellationToken)
    {
        await _context.Bookings.AddAsync(booking, cancellationToken);
        return booking.Id.Value;
    }

    public async Task<bool> IsRoomAvailable(
        RoomId roomId,
        DateRange period,
        CancellationToken cancellationToken)
    {
        return !await _context.Bookings
            .Where(b => b.RoomId == roomId)
            .Where(b => b.Status == BookingStatus.ACTIVE)
            .Where(b => b.Period.CheckIn < period.CheckOut && b.Period.CheckOut > period.CheckIn)
            .AnyAsync(cancellationToken);
    }
}
