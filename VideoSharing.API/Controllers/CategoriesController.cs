// <copyright file="CategoriesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static System.String;
using static AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.Category;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.EntityFrameworkCore.EntityState;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Controllers;

/// <summary>
/// Represents the API controller for the <see cref="Category"/> model.
/// </summary>
/// <seealso cref="ControllerBase"/>
[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly VideoSharingContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesController"/> class.
    /// </summary>
    /// <param name="context">The <see cref="VideoSharingContext"/> instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is null.</exception>
    public CategoriesController(VideoSharingContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Gets the paginated view model of the <see cref="Category"/> model.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="search">The search term.</param>
    /// <returns>The paginated view model of the <see cref="Category"/> model.</returns>
    [HttpGet]
    public async Task<PaginatedItemsViewModel<Category>> GetPaginatedCategories(int page = 0, string? search = null)
    {
        var query = this.context.Categories.AsQueryable();

        if (!IsNullOrEmpty(search))
        {
            query = query.Where(c => c.Name.Contains(search));
        }

        return await PaginatedItemsViewModel<Category>.CreateAsync(query, page);
    }

    /// <summary>
    /// Gets the <see cref="Category"/> model by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Category"/> model.</param>
    /// <returns>Action result of the <see cref="Category"/> model.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await this.context.Categories.FindAsync(id);

        if (category == null)
        {
            return this.NotFound();
        }

        return category;
    }

    /// <summary>
    /// Creates a new <see cref="Category"/> model.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> model.</param>
    /// <returns>Action result of the <see cref="Category"/> model.</returns>
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        await this.context.AddAsync(category);
        await this.context.SaveChangesAsync();

        return this.CreatedAtAction(nameof(this.GetCategory), new { id = category.Id }, category);
    }

    /// <summary>
    /// Updates the <see cref="Category"/> model.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Category"/> model.</param>
    /// <param name="category">The <see cref="Category"/> model.</param>
    /// <returns>Action result of the <see cref="Category"/> model.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status422UnprocessableEntity)]
    public async Task<ActionResult<Category>> PutCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return this.BadRequest();
        }

        if (id == DefaultCategoryId)
        {
            return this.UnprocessableEntity();
        }

        this.context.Entry(category).State = Modified;

        try
        {
            await this.context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!this.CategoryExists(id))
        {
            return this.NotFound();
        }

        return category;
    }

    /// <summary>
    /// Deletes the <see cref="Category"/> model.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Category"/> model.</param>
    /// <returns>Action result.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id == Category.DefaultCategoryId)
        {
            return this.UnprocessableEntity();
        }

        var category = await this.context.Categories.FindAsync(id);

        if (category == null)
        {
            return this.NotFound();
        }

        this.context.Remove(category);
        await this.context.SaveChangesAsync();

        return this.NoContent();
    }

    /// <summary>
    /// Gets the paginated view model of the <see cref="Video"/> model.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Category"/> model.</param>
    /// <param name="page">The page number.</param>
    /// <param name="search">The search term.</param>
    /// <returns>The paginated view model of the <see cref="Video"/> model.</returns>
    [HttpGet("{id}/Videos")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<PaginatedItemsViewModel<Video>>> GetPaginatedCategoryVideos(int id, int page = 0, string? search = null)
    {
        var category = await this.context.Categories.FindAsync(id);

        if (category == null)
        {
            return this.NotFound();
        }

        var query = this.context.Videos.Where(v => v.CategoryId == id);

        if (!IsNullOrEmpty(search))
        {
            query = query.Where(v => v.Title.Contains(search));
        }

        return await PaginatedItemsViewModel<Video>.CreateAsync(query, page);
    }

    /// <summary>
    /// Gets a fake collection of <see cref="Category"/> models.
    /// </summary>
    /// <returns>A fake collection of <see cref="Category"/> models.</returns>
    [AllowAnonymous]
    [HttpGet("Free")]
    public IEnumerable<Category> GetFakeCategories()
    {
        for (int i = 1; i <= 50; i++)
        {
            yield return new Category
            {
                Id = i,
                Name = $"Category {i}",
                Color = $"#{i:X6}",
            };
        }
    }

    private bool CategoryExists(int id) => this.context.Categories.Any(c => c.Id == id);

    private bool ContainsVideos(int id) => this.context.Videos.Any(v => v.CategoryId == id);
}