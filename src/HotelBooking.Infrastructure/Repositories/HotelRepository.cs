using CSharpFunctionalExtensions;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.ValueObjects.Ids;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Result<Hotel, Error>> GetById(HotelId id, CancellationToken cancellationToken)
    {
        var hotel = await _context.Hotels
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (hotel is null)
            return GeneralErrors.Entity.NotFound(nameof(Hotel), id.Value);

        return hotel;
    }

    public Task<UnitResult<Error>> Remove(Hotel hotel, CancellationToken cancellationToken)
    {
        _context.Hotels.Remove(hotel);

        return Task.FromResult(UnitResult.Success<Error>());
    }
}