// <copyright file="TestServerFixture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

/// <summary>
/// Represents the test server fixture.
/// </summary>
public class TestServerFixture
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestServerFixture"/> class.
    /// </summary>
    public TestServerFixture()
    {
        this.Client = CreateTestServer()
            .SeedDatabaseToTest()
            .CreateClient();
    }

    /// <summary>
    /// Gets the <see cref="HttpClient"/> to be used in the tests.
    /// </summary>
    public HttpClient Client { get; }

    private static TestServer CreateTestServer()
    {
        var webHostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json");
            })
            .ConfigureServices(services =>
            {
                services.AddDbContext<VideoSharingContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            })
            .ConfigureTestServices(services =>
            {
                services.AddAuthentication(defaultScheme: "TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        "TestScheme", options => { });
            })
            .UseStartup<Startup>();

        return new(webHostBuilder);
    }
}