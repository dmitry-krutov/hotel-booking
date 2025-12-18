namespace HotelBooking.Application.Features.Hotels.ReadModels;

public class HotelReadModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Country { get; set; }

    public required string City { get; set; }

    public string? Region { get; set; }

    public required string Street { get; set; }

    public string? Building { get; set; }

    public string? PostalCode { get; set; }

    public required string Description { get; set; }

    public ICollection<RoomReadModel> Rooms { get; set; } = new List<RoomReadModel>();
}
