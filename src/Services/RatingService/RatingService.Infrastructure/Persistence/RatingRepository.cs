using RatingService.Domain.Entities;
using RatingService.Domain.Interfaces;

namespace RatingService.Infrastructure.Persistence;

public class RatingRepository : IRatingRepository
{
    private readonly RatingContext ctx;

    public RatingRepository(RatingContext context)
    {
        ctx = context;
    }
    public void Add(Rating rating)
    {
        ctx.Ratings.Add(rating);
        ctx.SaveChanges();
    }
    public IEnumerable<Rating> GetByServiceProviderId(int serviceProviderId)
    {
        return ctx.Ratings
            .Where(r => r.ProviderId == serviceProviderId)
            .ToList();
    }
}