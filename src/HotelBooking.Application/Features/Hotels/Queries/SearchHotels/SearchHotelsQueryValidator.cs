using FluentValidation;
using HotelBooking.Contracts.Hotels;

namespace HotelBooking.Application.Features.Hotels.Queries.SearchHotels;

public class SearchHotelsQueryValidator : AbstractValidator<SearchHotelsQuery>
{
    private const int MAX_PAGE_SIZE = 100;

    public SearchHotelsQueryValidator()
    {
        RuleFor(x => x.CheckIn)
            .NotEmpty()
            .LessThan(x => x.CheckOut);

        RuleFor(x => x.CheckOut)
            .NotEmpty();

        RuleFor(x => x.Guests)
            .GreaterThan(0);

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(MAX_PAGE_SIZE);

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue);

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue);

        RuleFor(x => x)
            .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
            .WithMessage("MinPrice must be less than or equal to MaxPrice.");

        RuleFor(x => x.Sort)
            .IsInEnum();
    }
}
