namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

[Collection(nameof(TestServerFixtureCollection))]
public class CategoriesScenarios
{
    private const string CategoriesUrl = "/api/v1/categories";

    private readonly HttpClient _client;

    public CategoriesScenarios(TestServerFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task GetCategories_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync(CategoriesUrl);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategory_ReturnsOk()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _client.GetAsync($"{CategoriesUrl}/{id}");

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        // Act
        var response = await _client.GetAsync($"{CategoriesUrl}/{id}");

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostCategory_ReturnsCreated()
    {
        // Arrange
        var category = GetNewCategory();

        // Act
        var response = await _client.PostAsJsonAsync(CategoriesUrl, category);

        // Assert
        Assert.Equal(Created, response.StatusCode);
    }

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
        var response = await _client.PostAsJsonAsync(CategoriesUrl, category);

        // Assert
        Assert.Equal(BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutCategory_ReturnsOk()
    {
        // Arrange
        var category = GetExistingCategory();
        int id = category.Id;

        // Act
        var response = await _client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

        // Assert
        Assert.Equal(OK, response.StatusCode);
    }

    [Fact]
    public async Task PutCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var category = GetNonExistingCategory();
        int id = category.Id;

        // Act
        var response = await _client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

        // Assert
        Assert.Equal(NotFound, response.StatusCode);
    }

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
        var response = await _client.PutAsJsonAsync($"{CategoriesUrl}/{id}", category);

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
