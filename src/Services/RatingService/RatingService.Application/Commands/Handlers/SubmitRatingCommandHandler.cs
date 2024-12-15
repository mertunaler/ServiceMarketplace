using MediatR;
using RatingService.Application.Events;
using RatingService.Application.Interfaces;
using RatingService.Domain.Interfaces;
using RatingService.Domain.Entities;

namespace RatingService.Application.Commands.Handlers
{
    public class SubmitRatingCommandHandler : IRequestHandler<SubmitRatingCommand>
    {
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IEventPublisher _eventPublisher;

        public SubmitRatingCommandHandler(IServiceProviderRepository serviceProviderRepository,
            IRatingRepository ratingRepository)
        {
            _serviceProviderRepository = serviceProviderRepository;
            _ratingRepository = ratingRepository;
        }

        public Task Handle(SubmitRatingCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = _serviceProviderRepository.GetById(request.ProviderId);

            if (serviceProvider == null)
                throw new InvalidOperationException("Service provider not found.");

            var rating = new Rating(request.ProviderId, request.Score, request.Comment);
            _ratingRepository.Add(rating);

            var ratingSubmittedEvent = new RatingSubmitted
            {
                ProviderId = request.ProviderId,
                Score = request.Score,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _eventPublisher.PublishRatingSubmitted(ratingSubmittedEvent);
            return Task.CompletedTask;
        }
    }
}