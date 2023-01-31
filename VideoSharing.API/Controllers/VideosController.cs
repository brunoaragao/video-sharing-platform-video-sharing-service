using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;
using static System.String;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.EntityFrameworkCore.EntityState;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    private readonly VideoSharingContext _context;

    public VideosController(VideoSharingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // GET: api/v1/Videos[?page=1&size=5&search=]
    [HttpGet]
    [ProducesResponseType(Status200OK)]
    public async Task<PaginatedItemsViewModel<Video>> GetPaginatedVideosAsync(int page = 0, string? search = null)
    {
        var query = _context.Videos.AsQueryable();

        if (!IsNullOrWhiteSpace(search))
        {
            query = query.Where(v => v.Title.Contains(search));
        }

        const int size = 5;

        return await PaginatedItemsViewModel<Video>.CreateAsync(query, page, size);
    }

    // GET: api/v1/Videos/5
    [HttpGet("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Video>> GetVideoAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);

        if (video == null)
        {
            return NotFound();
        }

        return video;
    }

    // POST: api/v1/Videos
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<Video>> PostVideoAsync(Video video)
    {
        await _context.AddAsync(video);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVideoAsync), new { id = video.Id }, video);
    }

    // PUT: api/v1/Videos/5
    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Video>> PutVideoAsync(int id, Video video)
    {
        if (id != video.Id)
        {
            return BadRequest();
        }

        _context.Entry(video).State = Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!VideoExists(id))
        {
            return NotFound();
        }

        return video;
    }

    // DELETE: api/v1/Videos/5
    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IActionResult> DeleteVideoAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);

        if (video == null)
        {
            return NotFound();
        }

        _context.Remove(video);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/v1/Videos/Free
    [AllowAnonymous]
    [HttpGet("Free")]
    public IEnumerable<Video> GetFreeVideos()
    {
        for (int i = 0; i <= 500; i++)
        {
            yield return new Video
            {
                Id = i,
                Title = $"Video {i}",
                Description = $"Description {i}",
                Url = $"https://www.youtube.com/watch?v=video{i}",
                CategoryId = 1
            };
        }
    }


    private bool VideoExists(int id)
    {
        return _context.Videos.Any(e => e.Id == id);
    }

    private bool CategoryExists(int categoryId)
    {
        return _context.Categories.Any(e => e.Id == categoryId);
    }
}
