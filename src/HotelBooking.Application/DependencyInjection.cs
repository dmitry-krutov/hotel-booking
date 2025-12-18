using Core.Abstractions;
using Core.Mappings;
using FluentValidation;
using HotelBooking.Application.Features.Hotels.Commands.CreateHotel;
using HotelBooking.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AssemblyMappingProfile>();

            cfg.AddProfile<HotelsMappingProfile>();
            cfg.AddProfile<BookingsMappingProfile>();
            cfg.AddProfile<BookingsReadMappingProfile>();
        });

        services.AddHandlers();

        services.AddValidatorsFromAssemblyContaining<CreateHotelCommandHandler>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<CreateHotelCommandHandler>()
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandlerWithResult<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}