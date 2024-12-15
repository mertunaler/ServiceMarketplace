namespace RatingService.Domain.Entities;

public class ServiceProvider
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<Rating> Ratings { get; private set; }
    
}