using Core;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Bookings.Commands.CreateBooking;
using HotelBooking.Application.Features.Bookings.Queries.GetUserBookings;
using HotelBooking.Contracts.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

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

    [Authorize]
    [HttpGet]
    public async Task<EndpointResult<PagedList<UserBookingDto>>> GetUserBookings(
        [FromQuery] GetUserBookingsRequest request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IQueryHandlerWithResult<PagedList<UserBookingDto>, GetUserBookingsQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetUserBookingsQuery
        {
            UserId = currentUser.UserId,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return await handler.Handle(query, cancellationToken);
    }
}