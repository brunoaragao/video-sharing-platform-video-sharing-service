namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

[Collection(nameof(TestServerFixtureCollection))]
public class VideoScenarios
{
    private const string VideosUrl = "/api/v1/videos";

    private readonly HttpClient _client;

    public VideoScenarios(TestServerFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task GetVideos_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync(VideosUrl);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task GetVideo_ReturnsOk()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _client.GetAsync($"{VideosUrl}/{id}");

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task GetVideo_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        // Act
        var response = await _client.GetAsync($"{VideosUrl}/{id}");

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostVideo_ReturnsCreated()
    {
        // Arrange
        var video = GetNewVideo();

        // Act
        var response = await _client.PostAsJsonAsync(VideosUrl, video);

        // Assert
        Assert.Equal(Created, response.StatusCode);
    }

    [Theory]
    [InlineData(null, "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData("", "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData(" ", "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", null, "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", "", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", " ", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", "Description 1", null)]
    [InlineData("Video 1", "Description 1", "")]
    [InlineData("Video 1", "Description 1", " ")]
    [InlineData("Video 1", "Description 1", "invalid-url")]
    public async Task PostVideo_WithInvalidValues_ReturnsBadRequest(string title, string description, string url)
    {
        // Arrange
        var video = new Video { Title = title, Description = description, Url = url };

        // Act
        var response = await _client.PostAsJsonAsync(VideosUrl, video);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutVideo_ReturnsOk()
    {
        // Arrange
        var video = GetExistingVideo();
        int id = video.Id;

        // Act
        var response = await _client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task PutVideo_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var video = GetNonExistingVideo();
        int id = video.Id;

        // Act
        var response = await _client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData(null, "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData("", "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData(" ", "Description 1", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", null, "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", "", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", " ", "https://www.youtube.com/watch?v=1")]
    [InlineData("Video 1", "Description 1", null)]
    [InlineData("Video 1", "Description 1", "")]
    [InlineData("Video 1", "Description 1", " ")]
    [InlineData("Video 1", "Description 1", "invalid-url")]
    public async Task PutVideo_WithInvalidValues_ReturnsBadRequest(string title, string description, string url)
    {
        // Arrange
        var video = new Video { Title = title, Description = description, Url = url };
        int id = 1;

        // Act
        var response = await _client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutVideo_WithNonMatchingId_ReturnsBadRequest()
    {
        // Arrange
        var video = GetExistingVideo();
        int id = video.Id + 1;

        // Act
        var response = await _client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    private Video GetNewVideo()
    {
        return new() { Title = "Video 6", Description = "Description 6", Url = "https://www.youtube.com/watch?v=6", };
    }

    private Video GetExistingVideo()
    {
        return new() { Id = 1, Title = "Video 1", Description = "Description 1", Url = "https://www.youtube.com/watch?v=1", };
    }

    private Video GetNonExistingVideo()
    {
        return new() { Id = 999, Title = "Video 999", Description = "Description 999", Url = "https://www.youtube.com/watch?v=999", };
    }
}
