using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Entities;
using RatingService.Domain.Interfaces;

namespace RatingService.Infrastructure.Persistence;

public class ServiceProviderRepository : IServiceProviderRepository
{
    private readonly RatingContext ctx;

    public ServiceProviderRepository(RatingContext dbContext)
    {
        ctx = dbContext;
    }
    public ServiceProvider GetById(int serviceProviderId)
    {
        return ctx.ServiceProviders
            .Include(sp => sp.Ratings)
            .FirstOrDefault(sp => sp.Id == serviceProviderId);
    }
    public void AddProvider(ServiceProvider provider)
    {
        ctx.ServiceProviders.Add(provider);
        ctx.SaveChanges();
    }
}