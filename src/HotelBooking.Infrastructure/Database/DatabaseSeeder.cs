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
        var hotels = new List<Hotel>
        {
            CreateHotel(
                "Seaside Escape",
                "Spain",
                "Barcelona",
                "Carrer de la Marina 12",
                "Catalonia",
                "B2",
                "08005",
                "Modern beachside hotel with rooftop pool and quick access to Barceloneta.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Superior King", 180m, 3, true),
                    ("Garden View Double", 140m, 2, true),
                    ("Family Suite", 250m, 4, true),
                    ("Penthouse Terrace", 320m, 5, true),
                    ("Cozy Single", 90m, 1, true),
                    ("Sea Breeze Twin", 150m, 2, true),
                    ("Poolside Studio", 160m, 3, true)
                }),
            CreateHotel(
                "Mountain Retreat",
                "Switzerland",
                "Zurich",
                "Alpenstrasse 44",
                "Zurich",
                "3",
                "8001",
                "Quiet alpine stay with spa access and panoramic lake views.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Alpine Double", 160m, 2, true),
                    ("Mountain Suite", 230m, 4, true),
                    ("Ski Lodge Loft", 190m, 3, true),
                    ("Panorama King", 210m, 3, true),
                    ("Family Chalet", 260m, 5, true),
                    ("Budget Attic", 110m, 2, true)
                }),
            CreateHotel(
                "City Lights Hotel",
                "USA",
                "New York",
                "8th Avenue 215",
                "NY",
                null,
                "10011",
                "Urban landmark near theaters with skyline lounge and late checkout.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Executive Suite", 300m, 4, true),
                    ("City View King", 220m, 3, true),
                    ("Economy Single", 120m, 1, true),
                    ("Skyline Twin", 180m, 2, true),
                    ("Studio Plus", 150m, 2, true),
                    ("Grand Corner", 280m, 4, true),
                    ("Midtown Family", 240m, 5, true),
                    ("Penthouse Loft", 380m, 5, true)
                }),
            CreateHotel(
                "Lakeside Lodge",
                "Canada",
                "Toronto",
                "Bay Street 301",
                "Ontario",
                "9",
                "M5J 2N2",
                "Relaxed lodge on the waterfront promenade with bike rentals.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Lakeview King", 200m, 3, true),
                    ("Rustic Bunk", 130m, 3, true),
                    ("Waterfront Suite", 260m, 4, true),
                    ("Cabin Double", 150m, 2, true),
                    ("Couples Retreat", 170m, 2, true),
                    ("Garden Twin", 140m, 2, true)
                }),
            CreateHotel(
                "Desert Oasis Resort",
                "UAE",
                "Dubai",
                "Al Safa Street 77",
                "Dubai",
                "1A",
                "00000",
                "Resort with shaded courtyards, palm gardens, and quick metro access.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Premium Suite", 280m, 3, true),
                    ("Deluxe King", 220m, 3, true),
                    ("Villa with Patio", 260m, 4, true),
                    ("Courtyard Double", 160m, 2, true),
                    ("Business Studio", 180m, 2, true)
                }),
            CreateHotel(
                "Nordic Comfort",
                "Norway",
                "Oslo",
                "Karl Johans gate 12",
                "Oslo",
                "C",
                "0154",
                "Warm interiors with sauna access and fjord tram stop nearby.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Northern Lights King", 230m, 3, true),
                    ("Arctic Family", 260m, 5, true),
                    ("Cozy Single", 110m, 1, true),
                    ("Harbor Twin", 150m, 2, true),
                    ("Sauna Suite", 240m, 3, true)
                }),
            CreateHotel(
                "Coastal Breeze Inn",
                "Portugal",
                "Lisbon",
                "Rua Augusta 188",
                "Lisboa",
                "4",
                "1100-057",
                "Boutique inn near Alfama with terrace breakfasts and river views.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Ocean Deluxe", 210m, 3, true),
                    ("Standard Twin", 140m, 2, true),
                    ("Courtyard Suite", 190m, 3, true),
                    ("Budget Single", 95m, 1, true),
                    ("Sunset Family", 230m, 5, true)
                }),
            CreateHotel(
                "Urban Loft Suites",
                "Germany",
                "Berlin",
                "Potsdamer Strasse 88",
                "Berlin",
                "5",
                "10785",
                "Industrial loft vibe with shared workspaces and late-night café.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Studio Loft", 160m, 2, true),
                    ("Corner Loft", 190m, 3, true),
                    ("Artist Suite", 210m, 3, true),
                    ("Balcony Double", 170m, 2, true),
                    ("Penthouse Loft", 320m, 4, true),
                    ("Compact Single", 120m, 1, true)
                }),
            CreateHotel(
                "Vineyard Estate",
                "USA",
                "Napa",
                "Oak Lane 55",
                "CA",
                null,
                "94558",
                "Estate stay among vines with tasting room and garden trails.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Barrel Suite", 240m, 3, true),
                    ("Garden Cottage", 180m, 3, true),
                    ("Vineyard King", 210m, 2, true),
                    ("Estate Family", 260m, 5, true),
                    ("Cellar Studio", 150m, 2, true)
                }),
            CreateHotel(
                "Historic Palace Hotel",
                "Czech Republic",
                "Prague",
                "Karlova 7",
                "Prague",
                "2",
                "11000",
                "Restored palace with vaulted ceilings, steps from Old Town Square.",
                new List<(string Title, decimal PricePerNight, int Capacity, bool IsActive)>
                {
                    ("Royal Suite", 320m, 4, true),
                    ("Junior Suite", 240m, 3, true),
                    ("Classic Double", 170m, 2, true),
                    ("Heritage King", 210m, 3, true),
                    ("Courtyard Twin", 150m, 2, true),
                    ("Attic Single", 110m, 1, true)
                })
        };

        return hotels;
    }

    private static List<Booking> CreateBookings(IEnumerable<Hotel> hotels)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var bookings = new List<Booking>();

        Booking BuildBooking(string hotelTitle, string roomTitle, int checkInOffsetDays, int nights, int guests)
        {
            var hotel = hotels.First(h => h.Title.Value == hotelTitle);
            var room = hotel.Rooms.First(r => r.Title.Value == roomTitle);
            var checkIn = today.AddDays(checkInOffsetDays);
            var checkOut = checkIn.AddDays(nights);

            return new Booking(
                BookingId.NewId(),
                hotel.Id,
                room.Id,
                Guid.NewGuid(),
                DateRange.Create(checkIn, checkOut).Value,
                GuestsCount.Create(guests).Value,
                room.PricePerNight.Value * nights);
        }

        bookings.Add(BuildBooking("Seaside Escape", "Superior King", 5, 3, 2));
        bookings.Add(BuildBooking("Seaside Escape", "Family Suite", 12, 5, 4));
        bookings.Add(BuildBooking("Mountain Retreat", "Mountain Suite", 2, 4, 4));
        bookings.Add(BuildBooking("Mountain Retreat", "Ski Lodge Loft", 15, 6, 3));
        bookings.Add(BuildBooking("City Lights Hotel", "Executive Suite", 1, 2, 3));
        bookings.Add(BuildBooking("City Lights Hotel", "Economy Single", 8, 3, 1));
        bookings.Add(BuildBooking("City Lights Hotel", "Economy Single", 9, 2, 1));
        bookings.Add(BuildBooking("City Lights Hotel", "Midtown Family", 18, 4, 5));
        bookings.Add(BuildBooking("Lakeside Lodge", "Lakeview King", 12, 4, 3));
        bookings.Add(BuildBooking("Lakeside Lodge", "Rustic Bunk", 20, 2, 3));
        bookings.Add(BuildBooking("Desert Oasis Resort", "Villa with Patio", 6, 5, 4));
        bookings.Add(BuildBooking("Desert Oasis Resort", "Premium Suite", 18, 4, 3));
        bookings.Add(BuildBooking("Nordic Comfort", "Arctic Family", 9, 3, 5));
        bookings.Add(BuildBooking("Nordic Comfort", "Northern Lights King", 25, 5, 2));
        bookings.Add(BuildBooking("Coastal Breeze Inn", "Ocean Deluxe", 7, 6, 3));
        bookings.Add(BuildBooking("Coastal Breeze Inn", "Standard Twin", 14, 3, 2));
        bookings.Add(BuildBooking("Urban Loft Suites", "Corner Loft", 11, 2, 3));
        bookings.Add(BuildBooking("Urban Loft Suites", "Studio Loft", 3, 3, 2));
        bookings.Add(BuildBooking("Vineyard Estate", "Barrel Suite", 13, 2, 3));
        bookings.Add(BuildBooking("Historic Palace Hotel", "Royal Suite", 4, 2, 2));

        return bookings;
    }

    private static Hotel CreateHotel(
        string title,
        string country,
        string city,
        string street,
        string? region,
        string? building,
        string? postalCode,
        string description,
        IEnumerable<(string Title, decimal PricePerNight, int Capacity, bool IsActive)> rooms)
    {
        var hotel = new Hotel(
            HotelId.NewId(),
            Title.Create(title).Value,
            Address.Create(country, city, street, region, building, postalCode).Value,
            Description.Create(description).Value);

        foreach (var room in rooms)
        {
            hotel.AddRoom(
                Title.Create(room.Title).Value,
                PricePerNight.Create(room.PricePerNight).Value,
                Capacity.Create(room.Capacity).Value,
                room.IsActive);
        }

        return hotel;
    }
}
