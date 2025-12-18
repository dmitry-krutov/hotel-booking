using FluentValidation;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelById;

public class GetHotelByIdQueryValidator : AbstractValidator<GetHotelByIdQuery>
{
    public GetHotelByIdQueryValidator()
    {
        RuleFor(x => x.HotelId)
            .NotEmpty();
    }
}
