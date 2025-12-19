using AutoMapper;
using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Contracts.Hotels;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotelById;

public sealed class GetHotelByIdQueryHandler
    : IQueryHandlerWithResult<HotelDetailsDto, GetHotelByIdQuery>
{
    private readonly IHotelReadRepository _hotelReadRepository;
    private readonly IValidator<GetHotelByIdQuery> _validator;
    private readonly IMapper _mapper;

    public GetHotelByIdQueryHandler(
        IHotelReadRepository hotelReadRepository,
        IValidator<GetHotelByIdQuery> validator,
        IMapper mapper)
    {
        _hotelReadRepository = hotelReadRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<HotelDetailsDto, ErrorList>> Handle(
        GetHotelByIdQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotel = await _hotelReadRepository.GetById(query.HotelId, cancellationToken);
        if (hotel is null)
            return HotelErrors.Hotels.NotFound(query.HotelId).ToErrorList();

        return _mapper.Map<HotelDetailsDto>(hotel);
    }
}
