// <copyright file="Category.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

/// <summary>
/// Represents a category.
/// </summary>
public class Category
{
    /// <summary>
    /// Default category identifier.
    /// </summary>
    public const int DefaultCategoryId = 1;

    /// <summary>
    /// Gets or sets the category identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets the category color.
    /// </summary>
    [Required]
    [StringLength(7, MinimumLength = 7)]
    [RegularExpression("^#([A-Fa-f0-9]{6})$")]
    required public string Color { get; set; }

    /// <summary>
    /// Deconstructs the <see cref="Category"/> into its components.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="name">The category name.</param>
    /// <param name="color">The category color.</param>
    public void Deconstruct(out int id, out string name, out string color)
    {
        id = this.Id;
        name = this.Name;
        color = this.Color;
    }
}