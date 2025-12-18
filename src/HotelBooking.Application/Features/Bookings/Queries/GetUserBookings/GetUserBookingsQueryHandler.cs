using AutoMapper;
using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Contracts.Bookings;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings.Queries.GetUserBookings;

public sealed class GetUserBookingsQueryHandler
    : IQueryHandlerWithResult<PagedList<UserBookingDto>, GetUserBookingsQuery>
{
    private readonly IBookingReadRepository _bookingReadRepository;
    private readonly IValidator<GetUserBookingsQuery> _validator;
    private readonly IMapper _mapper;

    public GetUserBookingsQueryHandler(
        IBookingReadRepository bookingReadRepository,
        IValidator<GetUserBookingsQuery> validator,
        IMapper mapper)
    {
        _bookingReadRepository = bookingReadRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<UserBookingDto>, ErrorList>> Handle(
        GetUserBookingsQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var bookings = await _bookingReadRepository.GetUserBookings(
            query.UserId,
            query.PageNumber,
            query.PageSize,
            cancellationToken);

        var dto = bookings.Select(MapBooking);

        return Result.Success<PagedList<UserBookingDto>, ErrorList>(dto);
    }

    private UserBookingDto MapBooking(BookingReadModel booking)
    {
        var dto = _mapper.Map<UserBookingDto>(booking);
        dto.Hotel = _mapper.Map<BookingHotelDto>(booking.Hotel);
        dto.Room = _mapper.Map<BookingRoomDto>(booking.Room);

        return dto;
    }
}
