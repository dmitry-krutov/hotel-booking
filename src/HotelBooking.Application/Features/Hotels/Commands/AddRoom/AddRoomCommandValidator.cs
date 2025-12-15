using Core.Validation;
using FluentValidation;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;

namespace HotelBooking.Application.Features.Hotels.Commands.AddRoom;

public sealed class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
{
    public AddRoomCommandValidator()
    {
        RuleFor(x => x.HotelId)
            .MustBeValueObject(HotelId.TryCreate, (cmd, vo) => cmd.HotelIdVo = vo);

        RuleFor(x => x.Title)
            .MustBeValueObject(Title.Create, (cmd, vo) => cmd.TitleVo = vo);

        RuleFor(x => x.PricePerNight)
            .MustBeValueObject(PricePerNight.Create, (cmd, vo) => cmd.PricePerNightVo = vo);

        RuleFor(x => x.Capacity)
            .MustBeValueObject(Capacity.Create, (cmd, vo) => cmd.CapacityVo = vo);
    }
}