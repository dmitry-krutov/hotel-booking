using HotelBooking.Application.Features.Bookings;
using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace HotelBooking.Infrastructure.Repositories;

public class BookingReadRepository : IBookingReadRepository
{
    private readonly ApplicationReadDbContext _context;

    public BookingReadRepository(ApplicationReadDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<BookingReadModel>> GetUserBookings(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var baseQuery = _context.Bookings
            .AsNoTracking()
            .Where(b => b.UserId == userId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var bookings = await baseQuery
            .OrderByDescending(b => b.CreatedAtUtc)
            .ThenByDescending(b => b.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(b => b.Hotel)
            .Include(b => b.Room)
            .ToListAsync(cancellationToken);

        return new PagedList<BookingReadModel>(bookings, totalCount, pageNumber, pageSize);
    }
}