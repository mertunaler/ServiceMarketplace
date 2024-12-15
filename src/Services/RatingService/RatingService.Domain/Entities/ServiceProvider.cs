namespace RatingService.Domain.Entities;

public class ServiceProvider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}