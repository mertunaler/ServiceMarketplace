using RatingService.Domain.Entities;

namespace RatingService.Domain.Interfaces;

public interface IServiceProviderRepository
{
    ServiceProvider GetById(int id);
    void AddProvider(ServiceProvider provider);
}