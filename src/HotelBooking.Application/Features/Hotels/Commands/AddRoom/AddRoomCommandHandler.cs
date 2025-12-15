using AutoMapper;
using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Contracts.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Commands.AddRoom;

public sealed class AddRoomCommandHandler : ICommandHandler<RoomDto, AddRoomCommand>
{
    private readonly IValidator<AddRoomCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddRoomCommandHandler(
        IValidator<AddRoomCommand> validator,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _validator = validator;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RoomDto, ErrorList>> Handle(
        AddRoomCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotelResult = await _hotelRepository.GetById(command.HotelIdVo, cancellationToken);
        if (hotelResult.IsFailure)
            return hotelResult.Error.ToErrorList();

        var roomResult = hotelResult.Value.AddRoom(
            command.TitleVo,
            command.PricePerNightVo,
            command.CapacityVo,
            command.IsActive);
        if (roomResult.IsFailure)
            return roomResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RoomDto>(roomResult.Value);
    }
}