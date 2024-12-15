using MediatR;
using RatingService.Domain.Interfaces;
using RatingService.Domain.Entities;

namespace RatingService.Application.Commands.Handlers
{
    public class SubmitRatingCommandHandler : IRequestHandler<SubmitRatingCommand>
    {
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IRatingRepository _ratingRepository;

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

            return Task.CompletedTask;
        }
    }
}