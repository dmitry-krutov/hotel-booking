using HotelBooking.Domain.Booking;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HotelBooking.Infrastructure.Database;

public static class DatabaseSeeder
{
    private static readonly IReadOnlyList<HotelSeedData> HotelsSeedData =
    [
        new HotelSeedData(
            Guid.Parse("0f5a30bd-4b3a-43a9-9f0e-1914cf8c5ba3"),
            "Seaside Escape",
            "Spain",
            "Barcelona",
            "Carrer de la Marina 12",
            "Catalonia",
            "B2",
            "08005",
            "Modern beachside hotel with rooftop pool and quick access to Barceloneta.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("d4c82800-ff86-4313-8a52-30b7301e5373"), "Superior King", 180m, 3, true),
                new RoomSeedData(Guid.Parse("083ac1aa-2071-4dea-b7f5-4c2b15c5ea34"), "Garden View Double", 140m, 2, true),
                new RoomSeedData(Guid.Parse("45e5a5e5-a69b-4bc2-bf56-9ce48e4b6cc0"), "Family Suite", 250m, 4, true),
                new RoomSeedData(Guid.Parse("292c17d3-6fe1-4c9e-9593-5b18b3c204c3"), "Penthouse Terrace", 320m, 5, true),
                new RoomSeedData(Guid.Parse("6a2db2d0-1f06-4db3-bebd-0908b863d715"), "Cozy Single", 90m, 1, true),
                new RoomSeedData(Guid.Parse("fd1b7188-5c4f-4d4c-a86c-6ea6f531d60e"), "Sea Breeze Twin", 150m, 2, true),
                new RoomSeedData(Guid.Parse("e7e1aab1-dfd6-4015-8b02-76748c409558"), "Poolside Studio", 160m, 3, true)
            }),
        new HotelSeedData(
            Guid.Parse("d84e69a8-19c3-47a3-860a-0cdb25caa3dd"),
            "Mountain Retreat",
            "Switzerland",
            "Zurich",
            "Alpenstrasse 44",
            "Zurich",
            "3",
            "8001",
            "Quiet alpine stay with spa access and panoramic lake views.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("3c1cd4d8-2b6c-4d56-a812-2d4d84bc2bdf"), "Alpine Double", 160m, 2, true),
                new RoomSeedData(Guid.Parse("4f8c72f5-6d54-412b-b454-145bc9c4b8c3"), "Mountain Suite", 230m, 4, true),
                new RoomSeedData(Guid.Parse("63a2220c-001c-4828-a37c-721ad86b1a1e"), "Ski Lodge Loft", 190m, 3, true),
                new RoomSeedData(Guid.Parse("aaaf3f60-4f8e-4089-9e26-284b5d7e55a5"), "Panorama King", 210m, 3, true),
                new RoomSeedData(Guid.Parse("11de6c49-eb2a-4ea1-94cd-b39007ba228b"), "Family Chalet", 260m, 5, true),
                new RoomSeedData(Guid.Parse("d81fd149-8abb-4200-811e-81510417f7b8"), "Budget Attic", 110m, 2, true)
            }),
        new HotelSeedData(
            Guid.Parse("6382a1ed-00a8-43db-a663-00b6a1d65704"),
            "City Lights Hotel",
            "USA",
            "New York",
            "8th Avenue 215",
            "NY",
            null,
            "10011",
            "Urban landmark near theaters with skyline lounge and late checkout.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("b72bb9b4-22bc-4a9f-8d31-7d0356f4b55f"), "Executive Suite", 300m, 4, true),
                new RoomSeedData(Guid.Parse("2db77509-0c6b-46b5-8063-5f478c2d9d56"), "City View King", 220m, 3, true),
                new RoomSeedData(Guid.Parse("7ef726f1-43d6-40a6-a4ef-9af03f0db0ff"), "Economy Single", 120m, 1, true),
                new RoomSeedData(Guid.Parse("acb38f7b-183c-47f7-9cd2-0b1e93fe5ee6"), "Skyline Twin", 180m, 2, true),
                new RoomSeedData(Guid.Parse("fd08946f-0e26-47f0-a929-5530b3389343"), "Studio Plus", 150m, 2, true),
                new RoomSeedData(Guid.Parse("16084731-e3ac-470a-869d-950c3cb79a7d"), "Grand Corner", 280m, 4, true),
                new RoomSeedData(Guid.Parse("36b7f97e-2480-4f7e-8e79-783a30f9e746"), "Midtown Family", 240m, 5, true),
                new RoomSeedData(Guid.Parse("e8643a26-b5c3-45e6-bf1e-6d031e5540d9"), "Penthouse Loft", 380m, 5, true)
            }),
        new HotelSeedData(
            Guid.Parse("eec74ed7-88e4-4c46-a994-9a7d42370ec8"),
            "Lakeside Lodge",
            "Canada",
            "Toronto",
            "Bay Street 301",
            "Ontario",
            "9",
            "M5J 2N2",
            "Relaxed lodge on the waterfront promenade with bike rentals.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("a5f4ca07-187c-46e3-a1df-f17e9c733a21"), "Lakeview King", 200m, 3, true),
                new RoomSeedData(Guid.Parse("1991785b-2891-433e-a2ec-5d50953c0c5f"), "Rustic Bunk", 130m, 3, true),
                new RoomSeedData(Guid.Parse("1b2d83ae-71e5-4e0f-8470-3c96b8a5df14"), "Waterfront Suite", 260m, 4, true),
                new RoomSeedData(Guid.Parse("6f212cef-6c2c-4afb-9439-658f2bb43d79"), "Cabin Double", 150m, 2, true),
                new RoomSeedData(Guid.Parse("7b93f62c-5b89-4dc1-a00f-a4298515b156"), "Couples Retreat", 170m, 2, true),
                new RoomSeedData(Guid.Parse("46fe195b-7221-4b74-856a-48dcf208f469"), "Garden Twin", 140m, 2, true)
            }),
        new HotelSeedData(
            Guid.Parse("c6d69c1e-eeb3-428f-8a69-2bdbcd9470eb"),
            "Desert Oasis Resort",
            "UAE",
            "Dubai",
            "Al Safa Street 77",
            "Dubai",
            "1A",
            "00000",
            "Resort with shaded courtyards, palm gardens, and quick metro access.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("bb1991b0-6919-4ede-b370-5f5fff166f32"), "Premium Suite", 280m, 3, true),
                new RoomSeedData(Guid.Parse("fc93c517-91b9-4f14-91f0-531e0200fe43"), "Deluxe King", 220m, 3, true),
                new RoomSeedData(Guid.Parse("d6ad5ef0-d038-4f36-8dc9-9ea72a236369"), "Villa with Patio", 260m, 4, true),
                new RoomSeedData(Guid.Parse("fc92f6b8-6b10-4d1c-b57b-962553769638"), "Courtyard Double", 160m, 2, true),
                new RoomSeedData(Guid.Parse("3e8f31af-d1ac-4c2d-af6e-2819f12b0001"), "Business Studio", 180m, 2, true)
            }),
        new HotelSeedData(
            Guid.Parse("1f2a9610-0c5f-4ec2-8a17-86e2a40c5f3a"),
            "Nordic Comfort",
            "Norway",
            "Oslo",
            "Karl Johans gate 12",
            "Oslo",
            "C",
            "0154",
            "Warm interiors with sauna access and fjord tram stop nearby.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("8f0a08f0-3e80-4aa9-8338-a9b6d4bba627"), "Northern Lights King", 230m, 3, true),
                new RoomSeedData(Guid.Parse("01c419fd-7d17-4997-9e06-745c9296ffa3"), "Arctic Family", 260m, 5, true),
                new RoomSeedData(Guid.Parse("a6a2b283-7532-4f6c-80d9-6d7f5e36aaab"), "Cozy Single", 110m, 1, true),
                new RoomSeedData(Guid.Parse("ff85745e-2dc0-4ad5-a01e-cd588204bf5f"), "Harbor Twin", 150m, 2, true),
                new RoomSeedData(Guid.Parse("5d2e2d65-335d-4c8c-8c02-099da8b2840f"), "Sauna Suite", 240m, 3, true)
            }),
        new HotelSeedData(
            Guid.Parse("a8ed417a-8d09-4374-8550-69af0dd1fb1a"),
            "Coastal Breeze Inn",
            "Portugal",
            "Lisbon",
            "Rua Augusta 188",
            "Lisboa",
            "4",
            "1100-057",
            "Boutique inn near Alfama with terrace breakfasts and river views.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("8f3f60c4-a9f2-4ed0-a15b-812495b355f3"), "Ocean Deluxe", 210m, 3, true),
                new RoomSeedData(Guid.Parse("845f40a6-0db9-4af5-b58e-7078718d2cc5"), "Standard Twin", 140m, 2, true),
                new RoomSeedData(Guid.Parse("6e395fe7-6a1b-4a08-9fdc-5f013f3902b3"), "Courtyard Suite", 190m, 3, true),
                new RoomSeedData(Guid.Parse("4b6c871f-26ab-4d2b-84f1-9c75c1ac14be"), "Budget Single", 95m, 1, true),
                new RoomSeedData(Guid.Parse("e5da4b92-cbed-4919-a247-2b90c5e61826"), "Sunset Family", 230m, 5, true)
            }),
        new HotelSeedData(
            Guid.Parse("46f329dc-2cb3-4da5-be37-72911dba4646"),
            "Urban Loft Suites",
            "Germany",
            "Berlin",
            "Potsdamer Strasse 88",
            "Berlin",
            "5",
            "10785",
            "Industrial loft vibe with shared workspaces and late-night café.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("c5b25ac1-e8bc-4d35-a3b4-4c1f8e343e2b"), "Studio Loft", 160m, 2, true),
                new RoomSeedData(Guid.Parse("80bf1a6f-4f63-403d-8893-2acb478f3635"), "Corner Loft", 190m, 3, true),
                new RoomSeedData(Guid.Parse("70a6d610-e6c0-4b5f-8a20-90db31f973a2"), "Artist Suite", 210m, 3, true),
                new RoomSeedData(Guid.Parse("cfa8b80c-7425-42e5-9520-2b6d0a6a0636"), "Balcony Double", 170m, 2, true),
                new RoomSeedData(Guid.Parse("35a511d9-f34f-4a9b-b2a4-1ac544cdf2cc"), "Penthouse Loft", 320m, 4, true),
                new RoomSeedData(Guid.Parse("f03f8800-0c05-4f92-9c4f-ff197b452c3f"), "Compact Single", 120m, 1, true)
            }),
        new HotelSeedData(
            Guid.Parse("f511a6f8-3f07-4757-9f93-0cc2a6a4ae66"),
            "Vineyard Estate",
            "USA",
            "Napa",
            "Oak Lane 55",
            "CA",
            null,
            "94558",
            "Estate stay among vines with tasting room and garden trails.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("3e34c09d-9b1f-482d-b152-d6a6c1dd6c60"), "Barrel Suite", 240m, 3, true),
                new RoomSeedData(Guid.Parse("3992f5cb-5dab-4f4f-bd4a-76fb9445d86b"), "Garden Cottage", 180m, 3, true),
                new RoomSeedData(Guid.Parse("81700433-ddd6-47ff-b1a1-1536cf515acd"), "Vineyard King", 210m, 2, true),
                new RoomSeedData(Guid.Parse("754f7d6a-4a8c-4d50-9dcd-d3abcf1b1e8f"), "Estate Family", 260m, 5, true),
                new RoomSeedData(Guid.Parse("f566886b-3c9c-4c07-91b6-08bcc75a5c0b"), "Cellar Studio", 150m, 2, true)
            }),
        new HotelSeedData(
            Guid.Parse("3a91c337-2e12-4651-82ee-78a5628d176c"),
            "Historic Palace Hotel",
            "Czech Republic",
            "Prague",
            "Karlova 7",
            "Prague",
            "2",
            "11000",
            "Restored palace with vaulted ceilings, steps from Old Town Square.",
            new List<RoomSeedData>
            {
                new RoomSeedData(Guid.Parse("1139b28f-2b51-4ca4-9f8d-5ee011d047bf"), "Royal Suite", 320m, 4, true),
                new RoomSeedData(Guid.Parse("63c25c8d-8a50-429b-9a5b-29f28316b02d"), "Junior Suite", 240m, 3, true),
                new RoomSeedData(Guid.Parse("64b01a34-73f6-45c4-8267-e0cf33a9b033"), "Classic Double", 170m, 2, true),
                new RoomSeedData(Guid.Parse("0c7b79c7-22be-4706-a10c-b005d80e3799"), "Heritage King", 210m, 3, true),
                new RoomSeedData(Guid.Parse("5321f228-6e34-4e68-90f2-94206c1f27b8"), "Courtyard Twin", 150m, 2, true),
                new RoomSeedData(Guid.Parse("a5cb01d8-935b-4120-b770-7bf177269118"), "Attic Single", 110m, 1, true)
            })
    ];

    private static readonly IReadOnlyList<BookingSeedData> BookingsSeedData =
    [
        new BookingSeedData(Guid.Parse("d0c5d28b-fc1c-46e1-9ce3-3f35a7d9cbf1"), Guid.Parse("cc7c92f6-7a9f-4be7-b5c5-1c7e2158c4a0"), "Seaside Escape", "Superior King", 5, 3, 2),
        new BookingSeedData(Guid.Parse("d19f6a19-1119-440e-9b87-12c93f1b4373"), Guid.Parse("cc7c92f6-7a9f-4be7-b5c5-1c7e2158c4a0"), "Seaside Escape", "Family Suite", 12, 5, 4),
        new BookingSeedData(Guid.Parse("2d507f78-93bd-45f0-8cea-6100ed546620"), Guid.Parse("1a144a0d-2f24-4c3e-96f4-4057be1e5b3f"), "Mountain Retreat", "Mountain Suite", 2, 4, 4),
        new BookingSeedData(Guid.Parse("93c8cf30-44a4-4176-8d7e-e550333c2b95"), Guid.Parse("1a144a0d-2f24-4c3e-96f4-4057be1e5b3f"), "Mountain Retreat", "Ski Lodge Loft", 15, 6, 3),
        new BookingSeedData(Guid.Parse("930a6d43-55f2-4ae3-85c1-3cf1007f3e9f"), Guid.Parse("6dde6a5f-46c4-4bf9-9a8a-6863596c6af1"), "City Lights Hotel", "Executive Suite", 1, 2, 3),
        new BookingSeedData(Guid.Parse("c38b6788-b322-41fc-b10a-89f4ebe4c7e8"), Guid.Parse("6dde6a5f-46c4-4bf9-9a8a-6863596c6af1"), "City Lights Hotel", "Economy Single", 8, 3, 1),
        new BookingSeedData(Guid.Parse("3d5bcb6b-d76d-4b7b-b07c-a6f49eb4d54c"), Guid.Parse("6dde6a5f-46c4-4bf9-9a8a-6863596c6af1"), "City Lights Hotel", "Economy Single", 9, 2, 1),
        new BookingSeedData(Guid.Parse("d5799999-47e9-415b-8b1f-df9616cd80b7"), Guid.Parse("cc7c92f6-7a9f-4be7-b5c5-1c7e2158c4a0"), "City Lights Hotel", "Midtown Family", 18, 4, 5),
        new BookingSeedData(Guid.Parse("fdf49f03-e512-49e6-8b43-017f2a2041df"), Guid.Parse("7f40468e-0400-4652-b5fd-5e0bc35d0e80"), "Lakeside Lodge", "Lakeview King", 12, 4, 3),
        new BookingSeedData(Guid.Parse("894cbe8f-d57c-45db-9c72-0c77d677f92c"), Guid.Parse("7f40468e-0400-4652-b5fd-5e0bc35d0e80"), "Lakeside Lodge", "Rustic Bunk", 20, 2, 3),
        new BookingSeedData(Guid.Parse("7b4f94ef-e289-4fb8-a3f4-faf60cd8e233"), Guid.Parse("5f96e968-450a-4b36-b1f3-a3c92d1eacb4"), "Desert Oasis Resort", "Villa with Patio", 6, 5, 4),
        new BookingSeedData(Guid.Parse("5ac5511a-f0de-4931-b27b-e11ebbcd28e0"), Guid.Parse("5f96e968-450a-4b36-b1f3-a3c92d1eacb4"), "Desert Oasis Resort", "Premium Suite", 18, 4, 3),
        new BookingSeedData(Guid.Parse("89b7b62a-17a4-4a22-b34b-b7b6c7b9e4a3"), Guid.Parse("fe55f870-2f0c-4ef2-9ba6-0d80f189c31b"), "Nordic Comfort", "Arctic Family", 9, 3, 5),
        new BookingSeedData(Guid.Parse("59e85e8f-5013-42b6-890b-72301b61d4bd"), Guid.Parse("fe55f870-2f0c-4ef2-9ba6-0d80f189c31b"), "Nordic Comfort", "Northern Lights King", 25, 5, 2),
        new BookingSeedData(Guid.Parse("222b47f9-dc89-4adb-9d15-7e1a89015d2e"), Guid.Parse("fe55f870-2f0c-4ef2-9ba6-0d80f189c31b"), "Coastal Breeze Inn", "Ocean Deluxe", 7, 6, 3),
        new BookingSeedData(Guid.Parse("0030d2b2-9b43-4180-a24c-7d5656139acc"), Guid.Parse("6dde6a5f-46c4-4bf9-9a8a-6863596c6af1"), "Coastal Breeze Inn", "Standard Twin", 14, 3, 2),
        new BookingSeedData(Guid.Parse("f68a80df-2b31-4226-b132-7f8fa5746eca"), Guid.Parse("7f40468e-0400-4652-b5fd-5e0bc35d0e80"), "Urban Loft Suites", "Corner Loft", 11, 2, 3),
        new BookingSeedData(Guid.Parse("6f8a1d2a-3656-4f4d-8013-38083b18ce58"), Guid.Parse("1a144a0d-2f24-4c3e-96f4-4057be1e5b3f"), "Urban Loft Suites", "Studio Loft", 3, 3, 2),
        new BookingSeedData(Guid.Parse("a6d25dd1-b6fb-4d42-8a41-2fc177dfd034"), Guid.Parse("5f96e968-450a-4b36-b1f3-a3c92d1eacb4"), "Vineyard Estate", "Barrel Suite", 13, 2, 3),
        new BookingSeedData(Guid.Parse("2ebb9063-6134-4139-b23f-9aff8ddd19ef"), Guid.Parse("cc7c92f6-7a9f-4be7-b5c5-1c7e2158c4a0"), "Historic Palace Hotel", "Royal Suite", 4, 2, 2)
    ];

    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSeedOptions>>().Value;
        if (!options.EnableSeeding && !options.ClearDatabase)
            return;

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationWriteDbContext>();

        if (options.ClearDatabase)
            await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.MigrateAsync();

        if (!options.EnableSeeding)
            return;

        if (await dbContext.Hotels.AnyAsync())
            return;

        var hotels = CreateHotelsWithRooms();
        await dbContext.Hotels.AddRangeAsync(hotels);

        var bookings = CreateBookings(hotels);
        await dbContext.Bookings.AddRangeAsync(bookings);

        await dbContext.SaveChangesAsync();
    }

    private static List<Hotel> CreateHotelsWithRooms()
    {
        var hotels = new List<Hotel>();

        foreach (var seed in HotelsSeedData)
        {
            hotels.Add(CreateHotel(seed));
        }

        return hotels;
    }

    private static List<Booking> CreateBookings(IEnumerable<Hotel> hotels)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var bookings = new List<Booking>();

        Booking BuildBooking(BookingSeedData seed)
        {
            var hotel = hotels.First(h => h.Title.Value == seed.HotelTitle);
            var room = hotel.Rooms.First(r => r.Title.Value == seed.RoomTitle);
            var checkIn = today.AddDays(seed.CheckInOffsetDays);
            var checkOut = checkIn.AddDays(seed.Nights);

            return new Booking(
                BookingId.Create(seed.BookingId),
                hotel.Id,
                room.Id,
                seed.UserId,
                DateRange.Create(checkIn, checkOut).Value,
                GuestsCount.Create(seed.Guests).Value,
                room.PricePerNight.Value * seed.Nights);
        }

        foreach (var seed in BookingsSeedData)
        {
            bookings.Add(BuildBooking(seed));
        }

        return bookings;
    }

    private static Hotel CreateHotel(HotelSeedData seed)
    {
        var hotel = new Hotel(
            HotelId.Create(seed.Id),
            Title.Create(seed.Title).Value,
            Address.Create(seed.Country, seed.City, seed.Street, seed.Region, seed.Building, seed.PostalCode).Value,
            Description.Create(seed.Description).Value);

        foreach (var room in seed.Rooms)
        {
            hotel.AddRoom(
                RoomId.Create(room.Id),
                Title.Create(room.Title).Value,
                PricePerNight.Create(room.PricePerNight).Value,
                Capacity.Create(room.Capacity).Value,
                room.IsActive);
        }

        return hotel;
    }

    private record HotelSeedData(
        Guid Id,
        string Title,
        string Country,
        string City,
        string Street,
        string? Region,
        string? Building,
        string? PostalCode,
        string Description,
        List<RoomSeedData> Rooms);

    private record RoomSeedData(
        Guid Id,
        string Title,
        decimal PricePerNight,
        int Capacity,
        bool IsActive);

    private record BookingSeedData(
        Guid BookingId,
        Guid UserId,
        string HotelTitle,
        string RoomTitle,
        int CheckInOffsetDays,
        int Nights,
        int Guests);
}
