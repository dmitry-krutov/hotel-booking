using AutoMapper;
using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Contracts.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;

public sealed class GetHotelAvailabilityQueryHandler
    : IQueryHandlerWithResult<HotelDetailsDto, GetHotelAvailabilityQuery>
{
    private readonly IHotelAvailabilityReadRepository _hotelAvailabilityReadRepository;
    private readonly IValidator<GetHotelAvailabilityQuery> _validator;
    private readonly IMapper _mapper;

    public GetHotelAvailabilityQueryHandler(
        IHotelAvailabilityReadRepository hotelAvailabilityReadRepository,
        IValidator<GetHotelAvailabilityQuery> validator,
        IMapper mapper)
    {
        _hotelAvailabilityReadRepository = hotelAvailabilityReadRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<HotelDetailsDto, ErrorList>> Handle(
        GetHotelAvailabilityQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotel = await _hotelAvailabilityReadRepository.GetHotelWithAvailableRooms(query, cancellationToken);
        if (hotel is null)
            return GeneralErrors.Entity.NotFound("Hotel", query.HotelId).ToErrorList();

        return _mapper.Map<HotelDetailsDto>(hotel);
    }
}
