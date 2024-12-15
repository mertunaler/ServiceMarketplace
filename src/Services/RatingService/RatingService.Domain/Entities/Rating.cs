namespace RatingService.Domain.Entities;

public class Rating
{
    public int Id { get; private set; }
    public int ProviderId { get; private set; }
    public int Score { get; private set; }
    public string Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Rating()
    {
    }
    public Rating(int providerId, int score, string comment)
    {
        if (score < 1 || score > 5)
            throw new ArgumentException("Score must be between 1 and 5.", nameof(score));
        
        ProviderId = providerId;
        Score = score;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
}