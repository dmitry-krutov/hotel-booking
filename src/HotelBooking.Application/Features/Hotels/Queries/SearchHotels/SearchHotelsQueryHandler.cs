using AutoMapper;
using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Contracts.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Queries.SearchHotels;

public sealed class SearchHotelsQueryHandler
    : IQueryHandlerWithResult<PagedList<HotelSearchResultDto>, SearchHotelsQuery>
{
    private readonly IHotelSearchReadRepository _hotelSearchReadRepository;
    private readonly IValidator<SearchHotelsQuery> _validator;
    private readonly IMapper _mapper;

    public SearchHotelsQueryHandler(
        IHotelSearchReadRepository hotelSearchReadRepository,
        IValidator<SearchHotelsQuery> validator,
        IMapper mapper)
    {
        _hotelSearchReadRepository = hotelSearchReadRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<HotelSearchResultDto>, ErrorList>> Handle(
        SearchHotelsQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotels = await _hotelSearchReadRepository.SearchHotels(query, cancellationToken);
        var dto = hotels.Select(hotel => _mapper.Map<HotelSearchResultDto>(hotel));

        return dto;
    }
}
