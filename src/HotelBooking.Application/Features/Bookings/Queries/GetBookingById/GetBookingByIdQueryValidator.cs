using FluentValidation;

namespace HotelBooking.Application.Features.Bookings.Queries.GetBookingById;

public sealed class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
{
    public GetBookingByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.RequesterId)
            .NotEmpty();
    }
}
