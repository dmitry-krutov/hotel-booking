using FluentValidation;

namespace HotelBooking.Application.Features.Bookings.Queries.GetUserBookings;

public class GetUserBookingsQueryValidator : AbstractValidator<GetUserBookingsQuery>
{
    private const int MAX_PAGE_SIZE = 100;

    public GetUserBookingsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(MAX_PAGE_SIZE);
    }
}
