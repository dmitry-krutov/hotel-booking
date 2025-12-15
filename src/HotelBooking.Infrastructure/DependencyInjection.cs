using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'MySql' is not configured.");

        services.AddDbContext<ApplicationWriteDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(typeof(ApplicationWriteDbContext).Assembly.FullName);
                    mySqlOptions.EnableRetryOnFailure(5);
                });

            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging(configuration.GetValue<bool>("Ef:SensitiveLogging"));
        });

        return services;
    }
}