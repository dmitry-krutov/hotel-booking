using FluentValidation;

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
            .Must(query => query.CheckIn < query.CheckOut)
            .WithMessage("Check-in date must be earlier than check-out date.");
    }
}
