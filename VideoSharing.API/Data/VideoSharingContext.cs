// <copyright file="VideoSharingContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

using Microsoft.EntityFrameworkCore;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;

/// <summary>
/// Represents the database context for the Video Sharing API.
/// </summary>
public class VideoSharingContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoSharingContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public VideoSharingContext(DbContextOptions<VideoSharingContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for <see cref="Video"/> entities.
    /// </summary>
    public DbSet<Video> Videos { get; private set; } = null!;

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for <see cref="Category"/> entities.
    /// </summary>
public DbSet<Category> Categories { get; private set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoSharingContext).Assembly);
    }
}