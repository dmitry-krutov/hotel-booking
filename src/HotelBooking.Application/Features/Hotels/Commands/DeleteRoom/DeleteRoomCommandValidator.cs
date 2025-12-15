using Core.Validation;
using FluentValidation;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.DeleteRoom;

public sealed class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(x => x.HotelId)
            .MustBeValueObject(HotelId.TryCreate, (cmd, vo) => cmd.HotelIdVo = vo);

        RuleFor(x => x.RoomId)
            .MustBeValueObject(RoomId.TryCreate, (cmd, vo) => cmd.RoomIdVo = vo);
    }
}
