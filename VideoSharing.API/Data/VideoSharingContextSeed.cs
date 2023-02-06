// <copyright file="VideoSharingContextSeed.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

using Microsoft.EntityFrameworkCore;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;

/// <summary>
/// Represents the seed data for the Video Sharing API.
/// </summary>
public class VideoSharingContextSeed
{
    private readonly VideoSharingContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoSharingContextSeed"/> class.
    /// </summary>
    /// <param name="context">The <see cref="VideoSharingContext"/> to be used.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is <see langword="null"/>.</exception>
    public VideoSharingContextSeed(VideoSharingContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Seeds the database.
    /// </summary>
    public void Seed()
    {
        this.context.Database.Migrate();

        this.SeedCategories();
        this.SeedVideos();

        this.context.SaveChanges();
    }

    private void SeedCategories()
    {
        if (this.context.Categories.Any(c => c.Id != Category.DefaultCategoryId))
        {
            return;
        }

        this.context.AddRange(new Category[]
        {
            new() { Name = "Trailers", Color = "#000080" },
            new() { Name = "Movies", Color = "#FFFF00" },
            new() { Name = "Documentary", Color = "#008000" },
            new() { Name = "Animation", Color = "#808080" },
            new() { Name = "Gaming", Color = "#800000" },
        });
    }

    private void SeedVideos()
    {
        if (this.context.Videos.Any())
        {
            return;
        }

        this.context.AddRange(new Video[]
        {
            new() { Title = "The Matrix (1999) Official Trailer", Description = "The Matrix is a 1999 science fiction action film written and directed by ...", Url = "https://youtu.be/vKQi3bBA1y8" },
            new() { Title = "The Matrix Reloaded (2003) Official Trailer", Description = "The Matrix Reloaded is a 2003 science fiction action film written and directed by ...", Url = "https://youtu.be/kYzz0FSgpSU" },
            new() { Title = "The Matrix Revolutions (2003) Official Trailer", Description = "The Matrix Revolutions is a 2003 science fiction action film written and directed by ...", Url = "https://youtu.be/hMbexEPAOQI" },
            new() { Title = "The Matrix Resurrections – Official Trailer", Description = "The Matrix Resurrections is a 2022 science fiction action film written and directed by ...", Url = "https://youtu.be/9ix7TUGVYIo" },
            new() { Title = "The Matrix", Description = "The Matrix is a 1999 science fiction action film written and directed by ...", Url = "https://youtu.be/UvqDq2RLZdY" },
            new() { Title = "Matrix Reloaded", Description = "The Matrix Reloaded is a 2003 science fiction action film written and directed by ...", Url = "https://youtu.be/PbHo1obT1Kw" },
            new() { Title = "Matrix Revolutions", Description = "The Matrix Revolutions is a 2003 science fiction action film written and directed by ...", Url = "https://youtu.be/6f-7qGzhlD8" },
            new() { Title = "The Matrix Resurrections", Description = "The Matrix Resurrections is a 2022 science fiction action film written and directed by ...", Url = "https://youtu.be/O5rXlNRjyUE" },
            new() { Title = "The Matrix Revisited", Description = "The Matrix Revisited is a 2001 documentary film directed by ...", Url = "https://youtu.be/ld7Vikf_d1s" },
            new() { Title = "The Animatrix", Description = "The Animatrix is a 2003 animated anthology science fiction film written and directed by ...", Url = "https://youtu.be/V-62yBSkTRY" },
            new() { Title = "The Matrix Awakens: An Unreal Engine 5 Experience", Description = "The Matrix Awakens is a 2021 game developed by ...", Url = "https://youtu.be/WU0gvPcc3jQ" },
        });
    }
}