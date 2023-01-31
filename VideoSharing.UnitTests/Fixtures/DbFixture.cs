namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests.Fixtures;

public class DbFixture : IDisposable
{
    private readonly DbContextOptions<VideoSharingContext> _options;

    public DbFixture()
    {
        _options = new DbContextOptionsBuilder<VideoSharingContext>()
            .UseInMemoryDatabase("VideoSharing")
            .Options;

        using var context = new VideoSharingContext(_options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.AddRange(GetFakeVideos());
        context.AddRange(GetFakeCategories());
        context.SaveChanges();
    }

    public VideoSharingContext CreateContext()
    {
        return new(_options);
    }

    public void Dispose()
    {
        using var context = new VideoSharingContext(_options);
        context.Database.EnsureDeleted();
    }

    private static IEnumerable<Video> GetFakeVideos()
    {
        return new Video[]
        {
            new() { Title = "Video 1", Description = "Description 1", Url = "https://www.youtube.com/watch?v=1", CategoryId = 1 },
            new() { Title = "Video 2", Description = "Description 2", Url = "https://www.youtube.com/watch?v=2", CategoryId = 1 },
            new() { Title = "Video 3", Description = "Description 3", Url = "https://www.youtube.com/watch?v=3", CategoryId = 1 },
            new() { Title = "Video 4", Description = "Description 4", Url = "https://www.youtube.com/watch?v=4", CategoryId = 1 },
            new() { Title = "Video 5", Description = "Description 5", Url = "https://www.youtube.com/watch?v=5", CategoryId = 1 }
        };
    }

    private static IEnumerable<Category> GetFakeCategories()
    {
        return new Category[]
        {
            // Category 1 is the default category
            new() { Name = "Category 2", Color = "#000000" },
            new() { Name = "Category 3", Color = "#000000" },
            new() { Name = "Category 4", Color = "#000000" },
            new() { Name = "Category 5", Color = "#000000" }
        };
    }
}
