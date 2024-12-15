using MassTransit;
using RatingService.Application.Events;
using RatingService.Application.Interfaces;

namespace RatingService.Infrastructure.Messaging;

public class MassTransitPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public void PublishRatingSubmitted(RatingSubmitted eventMessage)
    {
        _publishEndpoint.Publish(eventMessage);
    }
}