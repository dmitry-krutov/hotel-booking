using AutoMapper;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Hotels.Commands.CreateHotel;
using HotelBooking.Application.Features.Hotels.Commands.DeleteHotel;
using HotelBooking.Application.Features.Hotels.Commands.UpdateHotel;
using HotelBooking.Contracts.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Web.Hotels;

public class HotelsController(IMapper mapper) : ApplicationController
{
    [HttpPost]
    public async Task<EndpointResult<HotelDto>> CreateHotel(
        [FromBody] CreateHotelRequest request,
        [FromServices] ICommandHandler<HotelDto, CreateHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateHotelCommand>(request);
        return await handler.Handle(command, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task<EndpointResult<HotelDto>> UpdateHotel(
        Guid id,
        [FromBody] UpdateHotelRequest request,
        [FromServices] ICommandHandler<HotelDto, UpdateHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateHotelCommand>(request);
        command.Id = id;

        return await handler.Handle(command, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task<EndpointResult> DeleteHotel(
        Guid id,
        [FromServices] ICommandHandler<DeleteHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteHotelCommand
        {
            Id = id,
        };

        return await handler.Handle(command, cancellationToken);
    }
}