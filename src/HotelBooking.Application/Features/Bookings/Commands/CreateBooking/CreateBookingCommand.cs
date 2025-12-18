using Core.Abstractions;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommand : ICommand
{
    public Guid HotelId { get; set; }

    public Guid RoomId { get; set; }

    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }

    public int Guests { get; set; }

    public Guid UserId { get; set; }

    public HotelId HotelIdVo { get; set; } = null!;

    public RoomId RoomIdVo { get; set; } = null!;

    public DateRange PeriodVo { get; set; } = null!;

    public GuestsCount GuestsVo { get; set; } = null!;
}