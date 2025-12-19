using Core.Validation;
using FluentValidation;
using Shared.Errors;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;

public class GetHotelAvailabilityQueryValidator : AbstractValidator<GetHotelAvailabilityQuery>
{
    public GetHotelAvailabilityQueryValidator()
    {
        RuleFor(q => q.HotelId)
            .NotEmpty();

        RuleFor(q => q.Guests)
            .GreaterThan(0);

        RuleFor(q => q.CheckIn)
            .NotEmpty();

        RuleFor(q => q.CheckOut)
            .NotEmpty();

        RuleFor(q => q)
            .Custom((query, context) =>
            {
                if (query.CheckIn >= query.CheckOut)
                {
                    context.AddFailure(nameof(query.CheckIn), HotelErrors.Validation.InvalidAvailabilityDateRange().Serialize());
                }
            });
    }
}
