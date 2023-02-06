// <copyright file="TestServerExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests.Extensions;

/// <summary>
/// Represents the extensions for the <see cref="TestServer"/> class.
/// </summary>
public static class TestServerExtensions
{
    /// <summary>
    /// Seeds the database with fake data.
    /// </summary>
    /// <param name="testServer">The <see cref="TestServer"/> to be used.</param>
    /// <returns>The <see cref="TestServer"/> with the seeded database.</returns>
    public static TestServer SeedDatabaseToTest(this TestServer testServer)
    {
        using (var scope = testServer.Host.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<VideoSharingContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(GetFakeCategories());
            context.AddRange(GetFakeVideos());
            context.SaveChanges();
        }

        return testServer;
    }

    private static IEnumerable<Category> GetFakeCategories()
    {
        return new Category[]
        {
            // Category 1 is the default category
            new() { Id = 2, Name = "Category 2", Color = "#000000" },
            new() { Id = 3, Name = "Category 3", Color = "#000000" },
            new() { Id = 4, Name = "Category 4", Color = "#000000" },
            new() { Id = 5, Name = "Category 5", Color = "#000000" },
        };
    }

    private static IEnumerable<Video> GetFakeVideos()
    {
        return new Video[]
        {
            new() { Title = "Video 1", Description = "Description 1", CategoryId = 1, Url = "https://www.youtube.com/watch?v=1" },
            new() { Title = "Video 2", Description = "Description 2", CategoryId = 1, Url = "https://www.youtube.com/watch?v=2" },
            new() { Title = "Video 3", Description = "Description 3", CategoryId = 1, Url = "https://www.youtube.com/watch?v=3" },
            new() { Title = "Video 4", Description = "Description 4", CategoryId = 1, Url = "https://www.youtube.com/watch?v=4" },
            new() { Title = "Video 5", Description = "Description 5", CategoryId = 1, Url = "https://www.youtube.com/watch?v=5" },
        };
    }
}