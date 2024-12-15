using NSubstitute;
using RatingService.Application.Commands;
using RatingService.Application.Commands.Handlers;
using RatingService.Domain.Entities;
using RatingService.Domain.Interfaces;

namespace RatingService.Application.Tests;

public class SubmitRatingCommandHandlerTests
{
    private readonly IServiceProviderRepository _serviceProviderRepository;
    private readonly IRatingRepository _ratingRepository;
    private readonly SubmitRatingCommandHandler _handler;

    public SubmitRatingCommandHandlerTests()
    {
        _serviceProviderRepository = Substitute.For<IServiceProviderRepository>();
        _ratingRepository = Substitute.For<IRatingRepository>();
        _handler = new SubmitRatingCommandHandler(_serviceProviderRepository, _ratingRepository);
    }

    [Fact]
    public async Task Handle_ServiceProviderExists_AddsRating()
    {
        var request = new SubmitRatingCommand
        {
            ProviderId = 1,
            Comment = "Great service!",
            Score = 5
        };
        var serviceProvider = new ServiceProvider { Id = 1 };

        _serviceProviderRepository.GetById(1).Returns(serviceProvider);

        await _handler.Handle(request, CancellationToken.None);

        _ratingRepository.Received(1).Add(Arg.Is<Rating>(r =>
            r.ProviderId == 1 &&
            r.Comment == "Great service!" &&
            r.Score == 5));
    }

    [Fact]
    public async Task Handle_ServiceProviderDoesNotExist_ThrowsInvalidOperationException()
    {
        var request = new SubmitRatingCommand
        {
            ProviderId = 1,
            Comment = "Poor service.",
            Score = 1
        };

        _serviceProviderRepository.GetById(1).Returns((ServiceProvider)null);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(request, CancellationToken.None));

        Assert.Equal("Service provider not found.", exception.Message);
        _ratingRepository.DidNotReceive().Add(Arg.Any<Rating>());
    }
}