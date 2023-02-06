// <copyright file="CategoriesScenarios.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

/// <summary>
/// Represents the categories scenarios.
/// </summary>
[Collection(nameof(TestServerFixtureCollection))]
public class CategoriesScenarios
{
    private const string CategoriesUrl = "/api/v1/categories";

    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesScenarios"/> class.
    /// </summary>
    /// <param name="fixture">The test server fixture.</param>
    /// <exception cref="ArgumentNullException">Thrown when the fixture is null.</exception>
    public CategoriesScenarios(TestServerFixture fixture)
    {
        ArgumentNullException.ThrowIfNull(fixture);
        this.client = fixture.Client;
    }

    /// <summary>
    /// Gets categories. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_ReturnsOk()
    {
        // Act
        var response = await this.client.GetAsync(CategoriesUrl);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Gets category. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategory_ReturnsOk()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await this.client.GetAsync($"{CategoriesUrl}/{id}");

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Gets category with non existing id. Returns not found.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        // Act
        var response = await this.client.GetAsync($"{CategoriesUrl}/{id}");

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    /// <summary>
    /// Posts category. Returns created.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PostCategory_ReturnsCreated()
    {
        // Arrange
        var category = this.GetNewCategory();

        // Act
        var response = await this.client.PostAsJsonAsync(CategoriesUrl, category);

        // Assert
        Assert.Equal(Created, response.StatusCode);
    }

    /// <summary>
    /// Posts category with invalid data. Returns bad request.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="color">The color.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Theory]
    [InlineData(null, "#000000")]
    [InlineData("", "#000000")]
    [InlineData(" ", "#000000")]
    [InlineData("Category", null)]
    [InlineData("Category", "")]
    [InlineData("Category", " ")]
    [InlineData("Category", "#00000")]
    [InlineData("Category", "#0000000")]
    [InlineData("Category", "#00000000")]
    [InlineData("Category", "000000")]
    public async Task PostCategory_WithInvalidData_ReturnsBadRequest(string name, string color)
    {
        // Arrange
        var category = new Category { Name = name, Color = color };

        // Act
        var response = await this.client.PostAsJsonAsync(CategoriesUrl, category);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Puts category. Returns ok.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_ReturnsOk()
    {
        // Arrange
        var category = this.GetExistingCategory();
        int id = category.Id;

        // Act
        var response = await this.client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    /// <summary>
    /// Puts category with non existing id. Returns not found.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var category = this.GetNonExistingCategory();
        int id = category.Id;

        // Act
        var response = await this.client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    /// <summary>
    /// Puts category with invalid data. Returns bad request.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="color">The color.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Theory]
    [InlineData(null, "#000000")]
    [InlineData("", "#000000")]
    [InlineData(" ", "#000000")]
    [InlineData("Category", null)]
    [InlineData("Category", "")]
    [InlineData("Category", " ")]
    [InlineData("Category", "#00000")]
    [InlineData("Category", "#0000000")]
    [InlineData("Category", "#00000000")]
    [InlineData("Category", "000000")]
    public async Task PutCategory_WithInvalidData_ReturnsBadRequest(string name, string color)
    {
        // Arrange
        var category = new Category { Name = name, Color = color };
        int id = category.Id;

        // Act
        var response = await this.client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    private Category GetNewCategory()
    {
        return new() { Name = "New Category", Color = "#000000" };
    }

    private Category GetExistingCategory()
    {
        return new() { Id = 2, Name = "Category 2", Color = "#000000" };
    }

    private Category GetNonExistingCategory()
    {
        return new() { Id = 999, Name = "Non Existing Category", Color = "#000000" };
    }
}