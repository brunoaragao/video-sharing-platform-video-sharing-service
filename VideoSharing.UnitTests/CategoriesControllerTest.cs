// <copyright file="CategoriesControllerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests;

/// <summary>
/// Represents the categories controller test.
/// </summary>
[Collection(nameof(DbFixtureCollection))]
public class CategoriesControllerTest
{
    private readonly DbFixture fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesControllerTest"/> class.
    /// </summary>
    /// <param name="fixture">The database fixture.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="fixture"/> is null.</exception>
    public CategoriesControllerTest(DbFixture fixture)
    {
        this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    /// <summary>
    /// Gets categories. Returns paginated categories.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_ReturnsPaginatedCategories()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories();

        // Assert
        Assert.NotNull(paginatedCategories);
    }

    /// <summary>
    /// Gets categories. Returns paginated categories with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_ReturnsPaginatedCategoriesWithCorrectProperties()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories();

        // Assert
        Assert.All(paginatedCategories.Items, category =>
        {
            Assert.InRange(category.Id, 1, int.MaxValue);
            Assert.NotNull(category.Name);
            Assert.NotNull(category.Color);
        });
    }

    /// <summary>
    /// Gets categories. Returns paginated categories with correct pagination.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_ReturnsPaginatedCategoriesWithCorrectPagination()
    {
        // Arrange
        const int expectedPageIndex = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItems = 5;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories();

        // Assert
        Assert.Equal(expectedPageIndex, paginatedCategories.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedCategories.TotalPages);
        Assert.Equal(expectedTotalItems, paginatedCategories.TotalItems);
    }

    /// <summary>
    /// Gets categories using page. Returns paginated categories.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingPage_ReturnsPaginatedCategoriesWithCorrectPagination()
    {
        // Arrange
        const int page = 0;

        const int expectedPageIndex = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItems = 5;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(page);

        // Assert
        Assert.Equal(expectedPageIndex, paginatedCategories.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedCategories.TotalPages);
        Assert.Equal(expectedTotalItems, paginatedCategories.TotalItems);
    }

    /// <summary>
    /// Gets categories using search. Returns paginated categories.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingSearch_ReturnsPaginatedCategories()
    {
        // Arrange
        const string search = "Category";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(search: search);

        // Assert
        Assert.NotNull(paginatedCategories);
    }

    /// <summary>
    /// Gets categories using search. Returns paginated categories with search result.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingSearch_ReturnsPaginatedCategoriesWithSearchResult()
    {
        // Arrange
        const string search = "Category";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(search: search);

        // Assert
        Assert.All(paginatedCategories.Items, category =>
        {
            Assert.Contains(search, category.Name, OrdinalIgnoreCase);
        });
    }

    /// <summary>
    /// Gets categories using search. Returns paginated categories with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingSearch_ReturnsPaginatedCategoriesWithCorrectProperties()
    {
        // Arrange
        const string search = "Category";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(search: search);

        // Assert
        Assert.All(paginatedCategories.Items, category =>
        {
            Assert.InRange(category.Id, 1, int.MaxValue);
            Assert.NotNull(category.Name);
            Assert.NotNull(category.Color);
        });
    }

    /// <summary>
    /// Gets categories using search. Returns paginated categories with correct pagination.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingSearch_ReturnsPaginatedCategoriesWithCorrectPagination()
    {
        // Arrange
        const string search = "Category";
        const int expectedPageIndex = 0;
        const int expectedTotalPages = 1;
        const int expectedTotalItems = 4;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(search: search);

        // Assert
        Assert.Equal(expectedPageIndex, paginatedCategories.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedCategories.TotalPages);
        Assert.Equal(expectedTotalItems, paginatedCategories.TotalItems);
    }

    /// <summary>
    /// Gets categories using search and page. Returns paginated categories.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategories_UsingSearchAndPage_ReturnsPaginatedCategoriesWithCorrectPagination()
    {
        // Arrange
        const string search = "Category";
        const int page = 2;
        const int expectedPageIndex = 2;
        const int expectedTotalPages = 1;
        const int expectedTotalItems = 4;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedCategories = await controller.GetPaginatedCategories(page, search);

        // Assert
        Assert.Equal(expectedPageIndex, paginatedCategories.PageIndex);
        Assert.Equal(expectedTotalPages, paginatedCategories.TotalPages);
        Assert.Equal(expectedTotalItems, paginatedCategories.TotalItems);
    }

    /// <summary>
    /// Gets categories using search and page. Returns paginated categories with search result.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategory_ReturnsCategory()
    {
        // Arrange
        const int id = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetCategory(id);

        // Assert
        Assert.IsType<Category>(result.Value);
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Gets categories using search and page. Returns paginated categories with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategory_ReturnsCategoryWithCorrectProperties()
    {
        // Arrange
        const int id = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetCategory(id);

        // Assert
        Assert.Equal(id, result.Value?.Id);
        Assert.NotNull(result.Value?.Name);
        Assert.NotNull(result.Value?.Color);
    }

    /// <summary>
    /// Gets categories using search and page. Returns paginated categories with correct pagination.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetCategory(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Puts category. Returns category.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_ReturnsCategory()
    {
        // Arrange
        var category = this.GetExistingCategory();
        var (id, name, color) = category;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.PutCategory(category.Id, category);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Category>>(result);
        var updatedCategory = Assert.IsType<Category>(actionResult.Value);
        Assert.Equal(id, updatedCategory.Id);
        Assert.Equal(name, updatedCategory.Name);
        Assert.Equal(color, updatedCategory.Color);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;
        const string name = "Category 999";
        const string color = "#000000";

        var category = new Category
        {
            Id = id,
            Name = name,
            Color = color,
        };

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.PutCategory(id, category);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_WithDifferentId_ReturnsBadRequest()
    {
        // Arrange
        const int id = 1;
        const int differentId = 2;

        var category = new Category
        {
            Id = differentId,
            Name = "Category 2",
            Color = "#000000",
        };

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.PutCategory(id, category);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task PutCategory_WithDefaultId_ReturnsUnprocessableEntity()
    {
        // Arrange
        var category = new Category
        {
            Id = DefaultCategoryId,
            Name = "Category 1",
            Color = "#000000",
        };

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.PutCategory(DefaultCategoryId, category);

        // Assert
        Assert.IsType<UnprocessableEntityResult>(result.Result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.DeleteCategory(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteCategory_WithDefaultId_ReturnsUnprocessableEntity()
    {
        // Arrange
        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.DeleteCategory(DefaultCategoryId);

        // Assert
        Assert.IsType<UnprocessableEntityResult>(result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_ReturnsPaginatedVideos()
    {
        // Arrange
        const int id = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id);

        // Assert
        Assert.NotNull(result.Value);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        const int id = 1;
        const int page = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id, page);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedItemsViewModel<Video>>>(result);
        var paginatedVideos = Assert.IsType<PaginatedItemsViewModel<Video>>(actionResult.Value);

        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.NotNull(video.Title);
            Assert.NotNull(video.Description);
            Assert.NotNull(video.Url);
        });
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        const int id = 999;
        const int page = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id, page);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_UsingSearch_ReturnsPaginatedVideos()
    {
        // Arrange
        const int id = 1;
        const string search = "Video";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var paginatedVideos = await controller.GetPaginatedCategoryVideos(id, search: search);

        // Assert
        Assert.NotNull(paginatedVideos);
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_UsingSearch_ReturnsPaginatedVideosWithCorrectProperties()
    {
        // Arrange
        const int id = 1;
        const string search = "Video";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id, search: search);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedItemsViewModel<Video>>>(result);
        var paginatedVideos = Assert.IsType<PaginatedItemsViewModel<Video>>(actionResult.Value);

        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.NotNull(video.Title);
            Assert.NotNull(video.Description);
            Assert.NotNull(video.Url);
        });
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_UsingSearch_ReturnsPaginatedVideosWithCorrectSearch()
    {
        // Arrange
        const int id = 1;
        const string search = "Video";

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id, search: search);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedItemsViewModel<Video>>>(result);
        var paginatedVideos = Assert.IsType<PaginatedItemsViewModel<Video>>(actionResult.Value);

        Assert.All(paginatedVideos.Items, video =>
        {
            Assert.Contains(search, video.Title, StringComparison.OrdinalIgnoreCase);
        });
    }

    /// <summary>
    /// Puts category. Returns category with correct properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetCategoryVideos_UsingPage_ReturnsPaginatedVideos()
    {
        // Arrange
        const int id = 1;
        const int page = 1;

        using var context = this.fixture.CreateContext();
        var controller = new CategoriesController(context);

        // Act
        var result = await controller.GetPaginatedCategoryVideos(id, page);

        // Assert
        Assert.NotNull(result.Value);
    }

    private Category[] GetFakeCategories()
    {
        return new Category[]
        {
            // Id = 1 is reserved for the "Other" category
            new() { Name = "Category 2", Color = "#000000" },
            new() { Name = "Category 3", Color = "#000000" },
            new() { Name = "Category 4", Color = "#000000" },
            new() { Name = "Category 5", Color = "#000000" },
        };
    }

    private Category GetExistingCategory()
    {
        return new Category
        {
            Id = 2,
            Name = "Category 2",
            Color = "#000000",
        };
    }
}