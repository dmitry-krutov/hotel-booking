using Core.Validation;
using FluentValidation;
using Shared.Errors;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;

public class GetHotelAvailabilityQueryValidator : AbstractValidator<GetHotelAvailabilityQuery>
{
    public GetHotelAvailabilityQueryValidator()
    {
        RuleFor(q => q.HotelId)
            .NotEmpty()
            .WithError(HotelErrors.Validation.HotelIdRequired());

        RuleFor(q => q.Guests)
            .GreaterThan(0)
            .WithError(HotelErrors.Validation.GuestsMustBePositive());

        RuleFor(q => q.CheckIn)
            .NotEmpty()
            .WithError(HotelErrors.Validation.CheckInRequired());

        RuleFor(q => q.CheckOut)
            .NotEmpty()
            .WithError(HotelErrors.Validation.CheckOutRequired());

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
