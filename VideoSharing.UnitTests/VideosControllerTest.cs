// <copyright file="VideosControllerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests;

/// <summary>
/// Represents the videos controller test.
/// </summary>
[Collection(nameof(DbFixtureCollection))]
public class VideosControllerTest
{
    private readonly DbFixture fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideosControllerTest"/> class.
    /// </summary>
    /// <param name="fixture">The database fixture.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="fixture"/> is null.</exception>
    public VideosControllerTest(DbFixture fixture)
    {
        this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideos()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync();

        // Assert
        Assert.NotNull(paginatedVideos);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync();

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.InRange(video.Id, 1, int.MaxValue);
            Assert.NotNull(video.Title);
            Assert.NotNull(video.Description);
            Assert.NotNull(video.Url);
            Assert.InRange(video.CategoryId, 1, int.MaxValue);
        });
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct pagination.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const int expectedPageIndex = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync();

        // Assert
        Assert.Equal(expectedPageIndex, paginatedVideos.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_UsingPage_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const int page = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(page);

        // Assert
        Assert.Equal(page, paginatedVideos.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithSearch()
    {
        // Arrange
        const string search = "Video";

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(search: search);

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.Contains(search, video.Title, OrdinalIgnoreCase);
        });
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        const string search = "Video";

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(search: search);

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.InRange(video.Id, 1, int.MaxValue);
            Assert.NotNull(video.Title);
            Assert.NotNull(video.Description);
            Assert.NotNull(video.Url);
            Assert.InRange(video.CategoryId, 1, int.MaxValue);
        });
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const string search = "Video";
        const int expectedPageIndex = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(search: search);

        // Assert
        Assert.Equal(expectedPageIndex, paginatedVideos.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearchAndPage_ReturnsPaginatedVideosWithSearch()
    {
        // Arrange
        const string search = "Video";
        const int page = 0;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(page, search);

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.Contains(search, video.Title, OrdinalIgnoreCase);
        });
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideoAsync_ReturnsVideo()
    {
        // Arrange
        const int id = 1;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.GetVideoAsync(id);

        // Assert
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideoAsync_ReturnsVideoWithCorrectProperties()
    {
        // Arrange
        const int id = 1;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.GetVideoAsync(id);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(id, result.Value.Id);
        Assert.NotNull(result.Value.Title);
        Assert.NotNull(result.Value.Description);
        Assert.NotNull(result.Value.Url);
        Assert.InRange(result.Value.CategoryId, 1, int.MaxValue);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.GetVideoAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideoAsync_ReturnsVideo()
    {
        // Arrange
        var video = this.GetExistingVideo();

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(video.Id, video);

        // Assert
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideoAsync_ReturnsVideoWithCorrectProperties()
    {
        // Arrange
        var video = this.GetExistingVideo();
        var (id, title, description, url, categoryId) = video;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(id, video);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(id, result.Value.Id);
        Assert.Equal(title, result.Value.Title);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(url, result.Value.Url);
        Assert.Equal(categoryId, result.Value.CategoryId);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideoAsync_WithDifferentId_ReturnsBadRequest()
    {
        // Arrange
        const int id = 1;
        const int differentId = 2;

        var video = new Video
        {
            Id = id,
            Title = "Video 1",
            Description = "Description 1",
            Url = "https://www.youtube.com/watch?v=1",
        };

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(differentId, video);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var video = this.GetNonExistingVideo();

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(video.Id, video);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.DeleteVideoAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Gets paginated videos. Returns paginated videos with correct properties.
    /// </summary>
    [Fact]
    public void GetFreeVideos_ReturnsVideos()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var videos = controller.GetFakeVideos();

        // Assert
        Assert.NotEmpty(videos);
    }

    private Video[] GetFakeVideos()
    {
        return new Video[]
        {
            new() { Title = "Video 1", Description = "Description 1", Url = "https://www.youtube.com/watch?v=1" },
            new() { Title = "Video 2", Description = "Description 2", Url = "https://www.youtube.com/watch?v=2" },
            new() { Title = "Video 3", Description = "Description 3", Url = "https://www.youtube.com/watch?v=3" },
            new() { Title = "Video 4", Description = "Description 4", Url = "https://www.youtube.com/watch?v=4" },
            new() { Title = "Video 5", Description = "Description 5", Url = "https://www.youtube.com/watch?v=5" },
        };
    }

    private Video GetExistingVideo()
    {
        return new Video
        {
            Id = 1,
            Title = "Video 1",
            Description = "Description 1",
            Url = "https://www.youtube.com/watch?v=1",
        };
    }

    private Video GetNonExistingVideo()
    {
        return new Video
        {
            Id = 6,
            Title = "Video 6",
            Description = "Description 6",
            Url = "https://www.youtube.com/watch?v=6",
        };
    }
}