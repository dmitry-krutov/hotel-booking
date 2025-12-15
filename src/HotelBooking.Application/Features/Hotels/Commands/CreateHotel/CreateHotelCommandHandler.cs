using AutoMapper;
using Core.Abstractions;
using Core.Database;
using Core.Validation;
using CSharpFunctionalExtensions;
using FluentValidation;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.ValueObjects.Ids;
using SharedKernel;

namespace HotelBooking.Application.Features.Hotels.Commands.CreateHotel;

public sealed class CreateHotelCommandHandler : ICommandHandler<HotelDto, CreateHotelCommand>
{
    private readonly IValidator<CreateHotelCommand> _validator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(
        IValidator<CreateHotelCommand> validator,
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
        CreateHotelCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var id = HotelId.NewId();

        var hotel = new Hotel(
            id,
            command.TitleVo,
            command.AddressVo,
            command.DescriptionVo);

        await _hotelRepository.Add(hotel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<HotelDto>(hotel);
    }
}