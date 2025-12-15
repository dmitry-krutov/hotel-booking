using Core.Validation;
using FluentValidation;
using HotelBooking.Domain.Hotel.ValueObjects;

namespace HotelBooking.Application.Features.Hotels.Commands.CreateHotel;

public sealed class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(x => x.Title)
            .MustBeValueObject(Title.Create, (cmd, vo) => cmd.TitleVo = vo);

        RuleFor(x => x.Description)
            .MustBeValueObject(Description.Create, (cmd, vo) => cmd.DescriptionVo = vo);

        RuleFor(x => x.Address)
            .NotNull();

        RuleFor(x => x.Address)
            .Custom((dto, context) =>
            {
                if (dto is null)
                    return;

                var cmd = (CreateHotelCommand)context.InstanceToValidate;

                var result = Address.Create(
                    dto.Country,
                    dto.City,
                    dto.Street,
                    dto.Region,
                    dto.Building,
                    dto.PostalCode);

                if (result.IsFailure)
                    context.AddFailure(result.Error.Serialize());
                else
                    cmd.AddressVo = result.Value;
            });
    }
}