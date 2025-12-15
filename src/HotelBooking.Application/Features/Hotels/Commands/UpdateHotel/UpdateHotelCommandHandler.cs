using AutoMapper;
using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Application.Features.Hotels;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Commands.UpdateHotel;

public sealed class UpdateHotelCommandHandler : ICommandHandler<HotelDto, UpdateHotelCommand>
{
    private readonly IValidator<UpdateHotelCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateHotelCommandHandler(
        IValidator<UpdateHotelCommand> validator,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _validator = validator;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<HotelDto, ErrorList>> Handle(
        UpdateHotelCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var hotelResult = await _hotelRepository.GetById(command.HotelId, cancellationToken);
        if (hotelResult.IsFailure)
            return hotelResult.ToErrorList();

        var hotel = hotelResult.Value;

        hotel.UpdateDetails(command.TitleVo, command.AddressVo, command.DescriptionVo);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<HotelDto>(hotel);
    }
}
