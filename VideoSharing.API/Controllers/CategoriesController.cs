using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;
using static System.String;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.EntityFrameworkCore.EntityState;
using static AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.Category;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly VideoSharingContext _context;

    public CategoriesController(VideoSharingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // GET: api/v1/Categories[?page=1&search=]
    [HttpGet]
    [ProducesResponseType(Status200OK)]
    public async Task<PaginatedItemsViewModel<Category>> GetCategories(int page = 0, string? search = null)
    {
        var query = _context.Categories.AsQueryable();

        if (!IsNullOrEmpty(search))
        {
            query = query.Where(c => c.Name.Contains(search));
        }

        const int pageSize = 5;

        return await PaginatedItemsViewModel<Category>.CreateAsync(query, page, pageSize);
    }

    // GET: api/v1/Categories/5
    [HttpGet("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    // POST: api/v1/Categories
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        await _context.AddAsync(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    // PUT: api/v1/Categories/5
    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status422UnprocessableEntity)]
    public async Task<ActionResult<Category>> PutCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        if (id == DefaultCategoryId)
        {
            return UnprocessableEntity();
        }

        _context.Entry(category).State = Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CategoryExists(id))
        {
            return NotFound();
        }

        return category;
    }

    // DELETE: api/v1/Categories/5
    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id == Category.DefaultCategoryId)
        {
            return UnprocessableEntity();
        }

        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/v1/Categories/5/Videos[?page=1&search=]
    [HttpGet("{id}/Videos")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<PaginatedItemsViewModel<Video>>> GetCategoryVideos(int id, int page = 0, string? search = null)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        var query = _context.Videos.Where(v => v.CategoryId == id);

        if (!IsNullOrEmpty(search))
        {
            query = query.Where(v => v.Title.Contains(search));
        }

        const int pageSize = 5;

        return await PaginatedItemsViewModel<Video>.CreateAsync(query, page, pageSize);
    }

    // GET: api/v1/Categories/Free
    [AllowAnonymous]
    [HttpGet("Free")]
    public IEnumerable<Category> FreeCategories()
    {
        for (int i = 1; i <= 50; i++)
        {
            yield return new Category
            {
                Id = i,
                Name = $"Category {i}",
                Color = $"#{i:X6}"
            };
        }
    }

    private bool CategoryExists(int id) => _context.Categories.Any(c => c.Id == id);

    private bool ContainsVideos(int id) => _context.Videos.Any(v => v.CategoryId == id);
}
