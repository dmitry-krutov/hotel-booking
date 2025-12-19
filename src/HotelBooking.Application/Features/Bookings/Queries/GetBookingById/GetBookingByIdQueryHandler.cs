using Core.Abstractions;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Application.Features.Auth;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings.Queries.GetBookingById;

public sealed class GetBookingByIdQueryHandler
    : IQueryHandlerWithResult<BookingReadModel, GetBookingByIdQuery>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IBookingReadRepository _bookingReadRepository;
    private readonly IValidator<GetBookingByIdQuery> _validator;

    public GetBookingByIdQueryHandler(
        IAuthorizationService authorizationService,
        IBookingReadRepository bookingReadRepository,
        IValidator<GetBookingByIdQuery> validator)
    {
        _authorizationService = authorizationService;
        _bookingReadRepository = bookingReadRepository;
        _validator = validator;
    }

    public async Task<Result<BookingReadModel, ErrorList>> Handle(
        GetBookingByIdQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var booking = await _bookingReadRepository.GetById(query.Id, cancellationToken);
        if (booking is null)
            return BookingErrors.Bookings.NotFound(query.Id).ToErrorList();

        var isAdmin = await _authorizationService.IsAdminAsync(query.RequesterId, cancellationToken);

        if (!isAdmin && booking.UserId != query.RequesterId)
            return BookingErrors.Bookings.AccessDenied().ToErrorList();

        return booking;
    }
}
