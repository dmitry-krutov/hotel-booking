using Core.Abstractions;
using Core.Validation;
using FluentValidation;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.HotelId)
            .MustBeValueObject(HotelId.TryCreate, (cmd, vo) => cmd.HotelIdVo = vo);

        RuleFor(x => x.RoomId)
            .MustBeValueObject(RoomId.TryCreate, (cmd, vo) => cmd.RoomIdVo = vo);

        RuleFor(x => x.Guests)
            .MustBeValueObject(GuestsCount.Create, (cmd, vo) => cmd.GuestsVo = vo);

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(GeneralErrors.Validation.ValueIsRequired(nameof(CreateBookingCommand.UserId)));

        RuleFor(x => x)
            .Custom((command, context) =>
            {
                var dateRangeResult = DateRange.Create(command.CheckIn, command.CheckOut);
                if (dateRangeResult.IsFailure)
                {
                    context.AddFailure(dateRangeResult.Error.Serialize());
                    return;
                }

                var today = DateOnly.FromDateTime(dateTimeProvider.UtcNow.Date);
                if (command.CheckIn < today)
                {
                    context.AddFailure(BookingErrors.CheckInInPast().Serialize());
                    return;
                }

                command.PeriodVo = dateRangeResult.Value;
            });
    }
}