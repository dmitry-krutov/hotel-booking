using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Infrastructure.ReadConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.DbContexts;

public class ApplicationReadDbContext : DbContext
{
    public ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
        : base(options)
    {
    }

    public DbSet<HotelReadModel> Hotels => Set<HotelReadModel>();

    public DbSet<RoomReadModel> Rooms => Set<RoomReadModel>();

    public DbSet<BookingReadModel> Bookings => Set<BookingReadModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationReadDbContext).Assembly,
            type => type.Namespace == typeof(BookingReadConfiguration).Namespace);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}
