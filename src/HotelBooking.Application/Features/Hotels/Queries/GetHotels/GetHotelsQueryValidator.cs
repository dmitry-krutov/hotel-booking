using FluentValidation;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotels;

public class GetHotelsQueryValidator : AbstractValidator<GetHotelsQuery>
{
    private const int MAX_PAGE_SIZE = 100;

    public GetHotelsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(MAX_PAGE_SIZE);
    }
}
