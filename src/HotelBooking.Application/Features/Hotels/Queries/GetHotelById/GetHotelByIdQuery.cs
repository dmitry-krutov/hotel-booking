using Core.Abstractions;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelById;

public sealed class GetHotelByIdQuery : IQuery
{
    public Guid HotelId { get; set; }
}
