using AutoMapper;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Hotels.Commands.CreateHotel;
using HotelBooking.Contracts.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Web.Hotels;

public class HotelsController(IMapper mapper) : ApplicationController
{
    [HttpPost]
    public async Task<EndpointResult<HotelDto>> AddComment(
        [FromBody] CreateHotelRequest request,
        [FromServices] ICommandHandler<HotelDto, CreateHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateHotelCommand>(request);
        return await handler.Handle(command, cancellationToken);
    }
}