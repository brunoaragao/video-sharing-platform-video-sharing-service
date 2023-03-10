// <copyright file="VideoScenarios.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

/// <summary>
/// Represents the video scenarios.
/// </summary>
[Collection(nameof(TestServerFixtureCollection))]
public class VideoScenarios
{
    private const string VideosUrl = "/api/v1/videos";

    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoScenarios"/> class.
    /// </summary>
    /// <param name="fixture">The test server fixture.</param>
    /// <exception cref="ArgumentNullException">Thrown when the fixture is null.</exception>
    public VideoScenarios(TestServerFixture fixture)
    {
        ArgumentNullException.ThrowIfNull(fixture);
        this.client = fixture.Client;
    }

    /// <summary>
    /// Gets videos. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideos_ReturnsOk()
    {
        // Act
        var response = await this.client.GetAsync(VideosUrl);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Gets video. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideo_ReturnsOk()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await this.client.GetAsync($"{VideosUrl}/{id}");

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Gets video with non existing id. Returns not found.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetVideo_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        // Act
        var response = await this.client.GetAsync($"{VideosUrl}/{id}");

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    /// <summary>
    /// Posts video. Returns created.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PostVideo_ReturnsCreated()
    {
        // Arrange
        var video = this.GetNewVideo();

        // Act
        var response = await this.client.PostAsJsonAsync(VideosUrl, video);

        // Assert
        Assert.Equal(Created, response.StatusCode);
    }

    /// <summary>
    /// Posts video with invalid values. Returns bad request.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <param name="url">The url.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
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
        var response = await this.client.PostAsJsonAsync(VideosUrl, video);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Puts video. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideo_ReturnsOk()
    {
        // Arrange
        var video = this.GetExistingVideo();
        int id = video.Id;

        // Act
        var response = await this.client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Puts video with non existing id. Returns not found.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideo_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var video = this.GetNonExistingVideo();
        int id = video.Id;

        // Act
        var response = await this.client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    /// <summary>
    /// Puts video with invalid values. Returns bad request.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <param name="url">The url.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
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
        var response = await this.client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Puts video with non matching id. Returns bad request.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutVideo_WithNonMatchingId_ReturnsBadRequest()
    {
        // Arrange
        var video = this.GetExistingVideo();
        int id = video.Id + 1;

        // Act
        var response = await this.client.PutAsJsonAsync($"{VideosUrl}/{id}", video);

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