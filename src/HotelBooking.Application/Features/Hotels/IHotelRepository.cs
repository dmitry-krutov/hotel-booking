using CSharpFunctionalExtensions;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels;

public interface IHotelRepository
{
    Task<Result<Guid, Error>> Add(Hotel hotel, CancellationToken cancellationToken);

    Task<Result<Hotel, Error>> GetById(HotelId id, CancellationToken cancellationToken);
}