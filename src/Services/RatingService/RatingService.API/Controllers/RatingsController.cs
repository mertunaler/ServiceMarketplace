using MediatR;
using Microsoft.AspNetCore.Mvc;
using RatingService.Application.Commands;
using RatingService.Application.Queries;

namespace RatingService.API.Controllers;

[ApiController]
[Route("api/service-providers/{serviceProviderId}/ratings")]
public class RatingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RatingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitRating(int serviceProviderId, [FromBody] SubmitRatingCommand command)
    {
        command.ProviderId = serviceProviderId; 
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("average")]
    public async Task<IActionResult> GetAverageRating(int serviceProviderId)
    {
        var req = new GetRatingQuery(serviceProviderId);
        var averageRating = await _mediator.Send(req);
        return Ok(new { ServiceProviderId = serviceProviderId, Rating = averageRating });
    }
}