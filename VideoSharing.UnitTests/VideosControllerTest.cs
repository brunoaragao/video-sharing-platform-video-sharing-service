namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests;

[Collection(nameof(DbFixtureCollection))]
public class VideosControllerTest
{
    private readonly DbFixture _fixture;

    public VideosControllerTest(DbFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideos()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync();

        // Assert
        Assert.NotNull(paginatedVideos);
    }

    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        using var context = _fixture.CreateContext();
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

    [Fact]
    public async Task GetPaginatedVideosAsync_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const int expectedPageIndex = 0;
        const int expectedPageSize = 5;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync();

        // Assert
        Assert.Equal(expectedPageIndex, paginatedVideos.PageIndex);
        Assert.Equal(expectedPageSize, paginatedVideos.PageSize);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }

    [Fact]
    public async Task GetPaginatedVideosAsync_UsingPage_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const int page = 0;
        const int expectedPageSize = 5;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(page);

        // Assert
        Assert.Equal(page, paginatedVideos.PageIndex);
        Assert.Equal(expectedPageSize, paginatedVideos.PageSize);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }

    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithSearch()
    {
        // Arrange
        const string search = "Video";

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(search: search);

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.Contains(search, video.Title, OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        const string search = "Video";

        using var context = _fixture.CreateContext();
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

    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearch_ReturnsPaginatedVideosWithCorrectPagination()
    {
        // Arrange
        const string search = "Video";
        const int expectedPageIndex = 0;
        const int expectedPageSize = 5;
        const int expectedTotalPages = 1;
        const int expectedTotalItens = 5;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(search: search);

        // Assert
        Assert.Equal(expectedPageIndex, paginatedVideos.PageIndex);
        Assert.Equal(expectedPageSize, paginatedVideos.PageSize);
        Assert.Equal(expectedTotalPages, paginatedVideos.TotalPages);
        Assert.Equal(expectedTotalItens, paginatedVideos.TotalItems);
    }



    [Fact]
    public async Task GetPaginatedVideosAsync_UsingSearchAndPage_ReturnsPaginatedVideosWithSearch()
    {
        // Arrange
        const string search = "Video";
        const int page = 0;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedVideosAsync(page, search);

        // Assert
        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.Contains(search, video.Title, OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task GetVideoAsync_ReturnsVideo()
    {
        // Arrange
        const int id = 1;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.GetVideoAsync(id);

        // Assert
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task GetVideoAsync_ReturnsVideoWithCorrectProperties()
    {
        // Arrange
        const int id = 1;

        using var context = _fixture.CreateContext();
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

    [Fact]
    public async Task GetVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.GetVideoAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PutVideoAsync_ReturnsVideo()
    {
        // Arrange
        var video = GetExistingVideo();

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(video.Id, video);

        // Assert
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task PutVideoAsync_ReturnsVideoWithCorrectProperties()
    {
        // Arrange
        var video = GetExistingVideo();
        var (id, title, description, url, categoryId) = video;

        using var context = _fixture.CreateContext();
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

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(differentId, video);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task PutVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var video = GetNonExistingVideo();

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.PutVideoAsync(video.Id, video);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteVideoAsync_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var result = await controller.DeleteVideoAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetFreeVideos_ReturnsVideos()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var controller = new VideosController(context);

        // Act
        var videos = controller.GetFreeVideos();

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
            new() { Title = "Video 5", Description = "Description 5", Url = "https://www.youtube.com/watch?v=5" }
        };
    }

    private Video GetExistingVideo()
    {
        return new Video
        {
            Id = 1,
            Title = "Video 1",
            Description = "Description 1",
            Url = "https://www.youtube.com/watch?v=1"
        };
    }

    private Video GetNonExistingVideo()
    {
        return new Video
        {
            Id = 6,
            Title = "Video 6",
            Description = "Description 6",
            Url = "https://www.youtube.com/watch?v=6"
        };
    }
}
