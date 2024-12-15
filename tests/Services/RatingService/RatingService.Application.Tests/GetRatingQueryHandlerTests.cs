using NSubstitute;
using RatingService.Application.Queries;
using RatingService.Application.Queries.Handlers;
using RatingService.Domain.Entities;
using RatingService.Domain.Interfaces;

namespace RatingService.Application.Tests;

public class GetRatingQueryHandlerTests
{
    private readonly IRatingRepository _ratingRepository;
    private readonly GetRatingQueryHandler _handler;

    public GetRatingQueryHandlerTests()
    {
        _ratingRepository = Substitute.For<IRatingRepository>();
        _handler = new GetRatingQueryHandler(_ratingRepository);
    }

    [Fact]
    public async Task CorrectAvgReturns()
    {
        var request = new GetRatingQuery(1);

        var ratings = new List<Rating>
        {
            new Rating(1, 4, "Çook iyi"),
            new Rating(1, 5, "Eh işte"),
            new Rating(1, 3, "Berbattttt")
        };

        _ratingRepository.GetByServiceProviderId(1).Returns(ratings);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.Equal(4.00, result);
    }

    [Fact]
    public async Task NoRatingsReturnsZeroAvg()
    {
        var request = new GetRatingQuery(1);

        _ratingRepository.GetByServiceProviderId(1).Returns(new List<Rating>());

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.Equal(0, result);
    }
}