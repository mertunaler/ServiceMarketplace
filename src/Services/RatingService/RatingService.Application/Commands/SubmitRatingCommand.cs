using MediatR;

namespace RatingService.Application.Commands;

public class SubmitRatingCommand : IRequest, IRequest<Unit>
{
    public int ProviderId { get; set; }
    public int Score { get; set; }
    public string? Comment { get; set; }
}