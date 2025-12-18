using AutoMapper;
using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Contracts.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Queries.GetHotels;

public sealed class GetHotelsQueryHandler
    : IQueryHandlerWithResult<PagedList<HotelDto>, GetHotelsQuery>
{
    private readonly IHotelReadRepository _hotelReadRepository;
    private readonly IValidator<GetHotelsQuery> _validator;
    private readonly IMapper _mapper;

    public GetHotelsQueryHandler(
        IHotelReadRepository hotelReadRepository,
        IValidator<GetHotelsQuery> validator,
        IMapper mapper)
    {
        _hotelReadRepository = hotelReadRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<HotelDto>, ErrorList>> Handle(
        GetHotelsQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotels = await _hotelReadRepository.GetHotels(
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        var dto = hotels.Select(hotel => _mapper.Map<HotelDto>(hotel));

        return Result.Success<PagedList<HotelDto>, ErrorList>(dto);
    }
}
