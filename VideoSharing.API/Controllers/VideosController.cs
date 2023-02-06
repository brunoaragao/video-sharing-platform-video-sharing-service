// <copyright file="VideosController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static System.String;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.EntityFrameworkCore.EntityState;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Controllers;

/// <summary>
/// Represents the API controller for the <see cref="Video"/> model.
/// </summary>
/// <seealso cref="ControllerBase"/>
[Route("api/v1/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    private readonly VideoSharingContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideosController"/> class.
    /// </summary>
    /// <param name="context">The <see cref="VideoSharingContext"/> instance.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is null.</exception>
    public VideosController(VideoSharingContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Gets the paginated view model of the <see cref="Video"/> model.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="search">The search term.</param>
    /// <returns>The paginated view model of the <see cref="Video"/> model.</returns>
    [HttpGet]
    public async Task<PaginatedItemsViewModel<Video>> GetPaginatedVideosAsync(int page = 0, string? search = null)
    {
        var query = this.context.Videos.AsQueryable();

        if (!IsNullOrWhiteSpace(search))
        {
            query = query.Where(v => v.Title.Contains(search));
        }

        return await PaginatedItemsViewModel<Video>.CreateAsync(query, page);
    }

    /// <summary>
    /// Gets the <see cref="Video"/> model with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The <see cref="Video"/> model identifier.</param>
    /// <returns>Action result of the <see cref="Video"/> model.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Video>> GetVideoAsync(int id)
    {
        var video = await this.context.Videos.FindAsync(id);

        if (video == null)
        {
            return this.NotFound();
        }

        return video;
    }

    /// <summary>
    /// Creates the <see cref="Video"/> model.
    /// </summary>
    /// <param name="video">The <see cref="Video"/> model.</param>
    /// <returns>Action result of the <see cref="Video"/> model.</returns>
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<Video>> PostVideoAsync(Video video)
    {
        await this.context.AddAsync(video);
        await this.context.SaveChangesAsync();

        return this.CreatedAtAction(nameof(this.GetVideoAsync), new { id = video.Id }, video);
    }

    /// <summary>
    /// Updates the <see cref="Video"/> model with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The <see cref="Video"/> model identifier.</param>
    /// <param name="video">The <see cref="Video"/> model.</param>
    /// <returns>Action result of the <see cref="Video"/> model.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<Video>> PutVideoAsync(int id, Video video)
    {
        if (id != video.Id)
        {
            return this.BadRequest();
        }

        this.context.Entry(video).State = Modified;

        try
        {
            await this.context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!this.VideoExists(id))
        {
            return this.NotFound();
        }

        return video;
    }

    /// <summary>
    /// Deletes the <see cref="Video"/> model with the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The <see cref="Video"/> model identifier.</param>
    /// <returns>Action result.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IActionResult> DeleteVideoAsync(int id)
    {
        var video = await this.context.Videos.FindAsync(id);

        if (video == null)
        {
            return this.NotFound();
        }

        this.context.Remove(video);
        await this.context.SaveChangesAsync();

        return this.NoContent();
    }

    /// <summary>
    /// Gets a fake collection of <see cref="Video"/> models.
    /// </summary>
    /// <returns>A fake collection of <see cref="Video"/> models.</returns>
    [AllowAnonymous]
    [HttpGet("Free")]
    public IEnumerable<Video> GetFakeVideos()
    {
        for (int i = 0; i <= 500; i++)
        {
            yield return new Video
            {
                Id = i,
                Title = $"Video {i}",
                Description = $"Description {i}",
                Url = $"https://www.youtube.com/watch?v=video{i}",
                CategoryId = 1,
            };
        }
    }

    private bool VideoExists(int id)
    {
        return this.context.Videos.Any(e => e.Id == id);
    }

    private bool CategoryExists(int categoryId)
    {
        return this.context.Categories.Any(e => e.Id == categoryId);
    }
}