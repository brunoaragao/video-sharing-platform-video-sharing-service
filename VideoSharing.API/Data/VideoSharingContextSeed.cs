using Microsoft.EntityFrameworkCore;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;

public class VideoSharingContextSeed
{
    private readonly VideoSharingContext _context;

    public VideoSharingContextSeed(VideoSharingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Seed()
    {
        _context.Database.Migrate();

        SeedCategories();
        SeedVideos();

        _context.SaveChanges();
    }

    private void SeedCategories()
    {
        if (_context.Categories.Any(c => c.Id != Category.DefaultCategoryId))
        {
            return;
        }

        _context.AddRange(new Category[]
        {
            new() { Name = "Trailers", Color = "#000080" },
            new() { Name = "Movies", Color = "#FFFF00" },
            new() { Name = "Documentary", Color = "#008000" },
            new() { Name = "Animation", Color = "#808080" },
            new() { Name = "Gaming", Color = "#800000" }
        });
    }

    private void SeedVideos()
    {
        if (_context.Videos.Any())
        {
            return;
        }

        _context.AddRange(new Video[]
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
