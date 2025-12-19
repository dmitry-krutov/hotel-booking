using Core.Abstractions;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;

public sealed class GetHotelAvailabilityQuery : IQuery
{
    public Guid HotelId { get; set; }

    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }

    public int Guests { get; set; }
}
