// <copyright file="DbFixture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests.Fixtures;

/// <summary>
/// Represents the database fixture.
/// </summary>
public class DbFixture : IDisposable
{
    private readonly DbContextOptions<VideoSharingContext> options;
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbFixture"/> class.
    /// </summary>
    public DbFixture()
    {
        this.options = new DbContextOptionsBuilder<VideoSharingContext>()
            .UseInMemoryDatabase("VideoSharing")
            .Options;

        using var context = new VideoSharingContext(this.options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.AddRange(GetFakeVideos());
        context.AddRange(GetFakeCategories());
        context.SaveChanges();
    }

    /// <summary>
    /// Creates a new <see cref="VideoSharingContext"/> instance.
    /// </summary>
    /// <returns>A new <see cref="VideoSharingContext"/> instance.</returns>
    public VideoSharingContext CreateContext()
    {
        return new(this.options);
    }

    /// <summary>
    /// Disposes the <see cref="DbFixture"/> instance.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the <see cref="DbFixture"/> instance.
    /// </summary>
    /// <param name="disposing">Whether the method is called from the <see cref="Dispose()"/> method.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                using var context = new VideoSharingContext(this.options);
                context.Database.EnsureDeleted();
            }

            this.disposedValue = true;
        }
    }

    /// <summary>
    /// Gets a collection of fake videos.
    /// </summary>
    /// <returns>A collection of fake videos.</returns>
    private static IEnumerable<Video> GetFakeVideos()
    {
        return new Video[]
        {
            new() { Title = "Video 1", Description = "Description 1", Url = "https://www.youtube.com/watch?v=1", CategoryId = 1 },
            new() { Title = "Video 2", Description = "Description 2", Url = "https://www.youtube.com/watch?v=2", CategoryId = 1 },
            new() { Title = "Video 3", Description = "Description 3", Url = "https://www.youtube.com/watch?v=3", CategoryId = 1 },
            new() { Title = "Video 4", Description = "Description 4", Url = "https://www.youtube.com/watch?v=4", CategoryId = 1 },
            new() { Title = "Video 5", Description = "Description 5", Url = "https://www.youtube.com/watch?v=5", CategoryId = 1 },
        };
    }

    /// <summary>
    /// Gets a collection of fake categories.
    /// </summary>
    /// <returns>A collection of fake categories.</returns>
    private static IEnumerable<Category> GetFakeCategories()
    {
        return new Category[]
        {
            // Category 1 is the default category
            new() { Name = "Category 2", Color = "#000000" },
            new() { Name = "Category 3", Color = "#000000" },
            new() { Name = "Category 4", Color = "#000000" },
            new() { Name = "Category 5", Color = "#000000" },
        };
    }
}