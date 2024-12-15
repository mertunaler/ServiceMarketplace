using RatingService.Domain.Entities;

namespace RatingService.Domain.Interfaces;

public interface IRatingRepository
{
    void Add(Rating rating);
    IEnumerable<Rating> GetByServiceProviderId(int serviceProviderId);
}