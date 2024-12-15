using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.Persistence;

public class RatingContext : DbContext
{
    public RatingContext(DbContextOptions<RatingContext> opts) : base(opts) { }
    public DbSet<ServiceProvider> ServiceProviders { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceProvider>().HasKey(x => x.Id);

        modelBuilder.Entity<Rating>().HasKey(x => x.Id);

        modelBuilder.Entity<Rating>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<ServiceProvider>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Rating>()
            .HasOne<ServiceProvider>()
            .WithMany(s => s.Ratings)
            .HasForeignKey(x => x.ProviderId);
    }
}