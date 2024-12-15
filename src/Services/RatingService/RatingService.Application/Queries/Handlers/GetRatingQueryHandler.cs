using MediatR;
using RatingService.Domain.Interfaces;

namespace RatingService.Application.Queries.Handlers;

public class GetRatingQueryHandler : IRequestHandler<GetRatingQuery, double>
{
    private readonly IRatingRepository _ratingRepository;

    public GetRatingQueryHandler(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public Task<double> Handle(GetRatingQuery request, CancellationToken cancellationToken)
    {
        double rating = 0;
        var ratings = _ratingRepository.GetByServiceProviderId(request.ProviderId);
        if (!ratings.Any())
            return Task.FromResult(rating);

        rating = Math.Round(ratings.Average(r => r.Score), 2);
        return Task.FromResult(rating);
    }
}