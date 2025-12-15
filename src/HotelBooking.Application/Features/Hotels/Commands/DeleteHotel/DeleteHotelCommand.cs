using Core.Abstractions;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand : ICommand
{
    public Guid Id { get; init; }

    public HotelId HotelIdVo { get; set; } = null!;
}
