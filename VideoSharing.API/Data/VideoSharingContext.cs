using Microsoft.EntityFrameworkCore;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;

public class VideoSharingContext : DbContext
{
    public VideoSharingContext(DbContextOptions<VideoSharingContext> options) : base(options)
    {
    }

    public DbSet<Video> Videos { get; private set; } = null!;
    public DbSet<Category> Categories { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoSharingContext).Assembly);
    }
}
