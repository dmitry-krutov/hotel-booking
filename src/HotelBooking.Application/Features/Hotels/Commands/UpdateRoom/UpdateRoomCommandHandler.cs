using AutoMapper;
using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Contracts.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Commands.UpdateRoom;

public sealed class UpdateRoomCommandHandler : ICommandHandler<RoomDto, UpdateRoomCommand>
{
    private readonly IValidator<UpdateRoomCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateRoomCommandHandler(
        IValidator<UpdateRoomCommand> validator,
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
        UpdateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotelResult = await _hotelRepository.GetById(command.HotelIdVo, cancellationToken);
        if (hotelResult.IsFailure)
            return hotelResult.Error.ToErrorList();

        var roomResult = hotelResult.Value.UpdateRoom(
            command.RoomIdVo,
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
