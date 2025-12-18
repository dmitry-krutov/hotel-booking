using Core;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Bookings.Commands.CreateBooking;
using HotelBooking.Contracts.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Web;

public class BookingsController : ApplicationController
{
    [Authorize]
    [HttpPost]
    public async Task<EndpointResult<BookingDto>> CreateBooking(
        [FromBody] CreateBookingRequest request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ICommandHandler<BookingDto, CreateBookingCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand
        {
            HotelId = request.HotelId,
            RoomId = request.RoomId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            Guests = request.Guests,
            UserId = currentUser.UserId
        };

        return await handler.Handle(command, cancellationToken);
    }
}
