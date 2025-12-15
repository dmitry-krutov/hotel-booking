using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Commands.DeleteRoom;

public sealed class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly IValidator<DeleteRoomCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
        IValidator<DeleteRoomCommand> validator,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return UnitResult.Failure<ErrorList>(validationResult.ToList());

        var hotelResult = await _hotelRepository.GetById(command.HotelIdVo, cancellationToken);
        if (hotelResult.IsFailure)
            return UnitResult.Failure<ErrorList>(hotelResult.Error.ToErrorList());

        var removalResult = hotelResult.Value.RemoveRoom(command.RoomIdVo);
        if (removalResult.IsFailure)
            return removalResult.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
