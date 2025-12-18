using HotelBooking.Application.Features.Hotels;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace HotelBooking.Infrastructure.Repositories;

public class HotelReadRepository : IHotelReadRepository
{
    private readonly ApplicationReadDbContext _context;

    public HotelReadRepository(ApplicationReadDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<HotelReadModel>> GetHotels(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var baseQuery = _context.Hotels
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var hotels = await baseQuery
            .OrderBy(h => h.Title)
            .ThenBy(h => h.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<HotelReadModel>(hotels, totalCount, pageNumber, pageSize);
    }

    public async Task<HotelReadModel?> GetById(Guid hotelId, CancellationToken cancellationToken)
    {
        return await _context.Hotels
            .AsNoTracking()
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == hotelId, cancellationToken);
    }
}
