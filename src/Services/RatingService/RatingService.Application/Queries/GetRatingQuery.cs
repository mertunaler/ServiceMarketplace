using MediatR;

namespace RatingService.Application.Queries;

public class GetRatingQuery : IRequest<double>
{
    public int ProviderId { get; set; }

    public GetRatingQuery(int providerId)
    {
        ProviderId = providerId;
    }
}