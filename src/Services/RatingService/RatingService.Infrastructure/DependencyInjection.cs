using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingService.Domain.Interfaces;
using RatingService.Infrastructure.Persistence;

namespace RatingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddRepositories();

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
}