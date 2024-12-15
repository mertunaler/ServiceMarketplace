namespace RatingService.Application.Events;

public class RatingSubmitted
{
    public int ProviderId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}