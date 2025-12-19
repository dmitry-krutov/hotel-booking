using AutoMapper;
using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Contracts.Bookings;
using HotelBooking.Domain.Booking;
using HotelBooking.Domain.ValueObjects.Ids;
using Shared.Errors;
using SharedKernel;

namespace HotelBooking.Application.Features.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommandHandler : ICommandHandler<BookingDto, CreateBookingCommand>
{
    private readonly IValidator<CreateBookingCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(
        IValidator<CreateBookingCommand> validator,
        IHotelRepository hotelRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _validator = validator;
        _hotelRepository = hotelRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<BookingDto, ErrorList>> Handle(
        CreateBookingCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotelResult = await _hotelRepository.GetById(command.HotelIdVo, cancellationToken);
        if (hotelResult.IsFailure)
            return hotelResult.Error.ToErrorList();

        var room = hotelResult.Value.Rooms.FirstOrDefault(r => r.Id == command.RoomIdVo);
        if (room is null)
            return BookingErrors.Rooms.NotFound(command.RoomId).ToErrorList();

        if (!room.IsActive)
            return BookingErrors.Rooms.Inactive(command.RoomId).ToErrorList();

        if (command.GuestsVo.Value > room.Capacity.Value)
            return BookingErrors.Validation.GuestsExceedCapacity(room.Capacity.Value).ToErrorList();

        var isAvailable = await _bookingRepository.IsRoomAvailable(
            command.RoomIdVo,
            command.PeriodVo,
            cancellationToken);

        if (!isAvailable)
            return BookingErrors.Rooms.Unavailable(command.CheckIn, command.CheckOut).ToErrorList();

        var totalPrice = room.PricePerNight.Value * command.PeriodVo.Nights;

        var booking = new Booking(
            BookingId.NewId(),
            command.HotelIdVo,
            command.RoomIdVo,
            command.UserId,
            command.PeriodVo,
            command.GuestsVo,
            totalPrice);

        await _bookingRepository.Add(booking, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BookingDto>(booking);
    }
}
