using CSharpFunctionalExtensions;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Domain.Hotel;
using HotelBooking.Infrastructure.DbContexts;
using SharedKernel;

namespace HotelBooking.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationWriteDbContext _context;

    public HotelRepository(ApplicationWriteDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid, Error>> Add(Hotel hotel, CancellationToken cancellationToken)
    {
        await _context.Hotels.AddAsync(hotel, cancellationToken);
        return hotel.Id.Value;
    }
}