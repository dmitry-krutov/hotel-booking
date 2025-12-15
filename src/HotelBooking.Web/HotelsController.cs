using AutoMapper;
using Core.Abstractions;
using Framework;
using Framework.EndpointResults;
using HotelBooking.Application.Features.Hotels.Commands.AddRoom;
using HotelBooking.Application.Features.Hotels.Commands.CreateHotel;
using HotelBooking.Application.Features.Hotels.Commands.DeleteHotel;
using HotelBooking.Application.Features.Hotels.Commands.DeleteRoom;
using HotelBooking.Application.Features.Hotels.Commands.UpdateHotel;
using HotelBooking.Application.Features.Hotels.Commands.UpdateRoom;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Web;

public class HotelsController(IMapper mapper) : ApplicationController
{
    [Authorize(Roles = RoleNames.ADMIN)]
    [HttpPost]
    public async Task<EndpointResult<HotelDto>> CreateHotel(
        [FromBody] CreateHotelRequest request,
        [FromServices] ICommandHandler<HotelDto, CreateHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateHotelCommand>(request);
        return await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = RoleNames.ADMIN)]
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

    [Authorize(Roles = RoleNames.ADMIN)]
    [HttpPost("{id:guid}/rooms")]
    public async Task<EndpointResult<RoomDto>> AddRoom(
        Guid id,
        [FromBody] AddRoomRequest request,
        [FromServices] ICommandHandler<RoomDto, AddRoomCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddRoomCommand>(request);
        command.HotelId = id;

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = RoleNames.ADMIN)]
    [HttpPut("{id:guid}/rooms/{roomId:guid}")]
    public async Task<EndpointResult<RoomDto>> UpdateRoom(
        Guid id,
        Guid roomId,
        [FromBody] UpdateRoomRequest request,
        [FromServices] ICommandHandler<RoomDto, UpdateRoomCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateRoomCommand>(request);
        command.HotelId = id;
        command.RoomId = roomId;

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = RoleNames.ADMIN)]
    [HttpDelete("{id:guid}/rooms/{roomId:guid}")]
    public async Task<EndpointResult> DeleteRoom(
        Guid id,
        Guid roomId,
        [FromServices] ICommandHandler<DeleteRoomCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand { HotelId = id, RoomId = roomId, };

        return await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = RoleNames.ADMIN)]
    [HttpDelete("{id:guid}")]
    public async Task<EndpointResult> DeleteHotel(
        Guid id,
        [FromServices] ICommandHandler<DeleteHotelCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteHotelCommand { Id = id, };

        return await handler.Handle(command, cancellationToken);
    }
}