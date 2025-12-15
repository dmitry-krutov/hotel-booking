using CSharpFunctionalExtensions;
using HotelBooking.Domain.Hotel;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels;

public interface IHotelRepository
{
    Task<Result<Guid, Error>> Add(Hotel hotel, CancellationToken cancellationToken);
}