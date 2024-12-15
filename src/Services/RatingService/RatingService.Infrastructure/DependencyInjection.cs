using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingService.Application.Interfaces;
using RatingService.Domain.Interfaces;
using RatingService.Infrastructure.Messaging;
using RatingService.Infrastructure.Persistence;

namespace RatingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddRepositories()
            .ConfigureMassTransitWithRabbitMq(configuration)
            .AddEventPublisher();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RatingContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlStr")));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();

        return services;
    }

    public static IServiceCollection ConfigureMassTransitWithRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) => { cfg.Host(configuration.GetConnectionString("RabbitMQ")); });
        });
        return services;
    }

    public static IServiceCollection AddEventPublisher(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, MassTransitPublisher>();
        return services;
    }
}