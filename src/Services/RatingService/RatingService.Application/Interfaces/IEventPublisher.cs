using RatingService.Application.Events;

namespace RatingService.Application.Interfaces;

public interface IEventPublisher
{
    void PublishRatingSubmitted(RatingSubmitted eventMessage);
}