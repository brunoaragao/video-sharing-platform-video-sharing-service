// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API;

/// <summary>
/// Represents the entry point of the application.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<VideoSharingContextSeed>();
            context.Seed();
        }

        host.Run();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="IHostBuilder"/> class.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>A new instance of the <see cref="IHostBuilder"/> class.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}