// <copyright file="Video.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

/// <summary>
/// Represents a video.
/// </summary>
public class Video
{
    /// <summary>
    /// Gets or sets the video identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the video title.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    required public string Title { get; set; }

    /// <summary>
    /// Gets or sets the video description.
    /// </summary>
    [Required]
    [StringLength(2_000, MinimumLength = 10)]
    required public string Description { get; set; }

    /// <summary>
    /// Gets or sets the video URL.
    /// </summary>
    [Required]
    [StringLength(2_048, MinimumLength = 10)]
    [Url]
    required public string Url { get; set; }

    /// <summary>
    /// Gets or sets the video category identifier.
    /// </summary>
    [Range(1, int.MaxValue)]
    [DefaultValue(Category.DefaultCategoryId)]
    public int CategoryId { get; set; } = Category.DefaultCategoryId;

    /// <summary>
    /// Deconstructs the <see cref="Video"/> into its components.
    /// </summary>
    /// <param name="id">The video identifier.</param>
    /// <param name="title">The video title.</param>
    /// <param name="description">The video description.</param>
    /// <param name="url">The video URL.</param>
    /// <param name="categoryId">The video category identifier.</param>
    public void Deconstruct(out int id, out string title, out string description, out string url, out int categoryId)
    {
        id = this.Id;
        title = this.Title;
        description = this.Description;
        url = this.Url;
        categoryId = this.CategoryId;
    }
}