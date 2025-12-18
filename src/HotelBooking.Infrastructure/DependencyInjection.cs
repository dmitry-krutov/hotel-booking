using Core.Abstractions;
using Core.Database;
using HotelBooking.Application.Features.Bookings;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Infrastructure.DbContexts;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HotelBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EfOptions>(
            configuration.GetSection(EfOptions.SECTION_NAME));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IBookingReadRepository, BookingReadRepository>();

        services.AddDbContext<ApplicationWriteDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("AppDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'AppDb' is not configured.");

            var efOptions = sp.GetRequiredService<IOptions<EfOptions>>().Value;

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(typeof(ApplicationWriteDbContext).Assembly.FullName);
                    mySqlOptions.EnableRetryOnFailure(5);
                });

            options.UseSnakeCaseNamingConvention();
            options.EnableDetailedErrors();

            if (efOptions.EnableSensitiveLogging)
                options.EnableSensitiveDataLogging();
        });

        services.AddDbContext<ApplicationReadDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("AppDb");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'AppDb' is not configured.");

            var efOptions = sp.GetRequiredService<IOptions<EfOptions>>().Value;

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(typeof(ApplicationWriteDbContext).Assembly.FullName);
                    mySqlOptions.EnableRetryOnFailure(5);
                });

            options.UseSnakeCaseNamingConvention();
            options.EnableDetailedErrors();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (efOptions.EnableSensitiveLogging)
                options.EnableSensitiveDataLogging();
        });

        return services;
    }
}