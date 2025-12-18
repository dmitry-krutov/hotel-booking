namespace HotelBooking.Contracts.Hotels;

public class HotelDetailsDto : HotelDto
{
    public ICollection<RoomDto> Rooms { get; init; } = new List<RoomDto>();
}
