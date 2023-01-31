namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

public class TestServerFixture
{
    public TestServerFixture()
    {
        Client = CreateTestServer()
            .SeedDatabaseToTest()
            .CreateClient();
    }

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

        return new TestServer(webHostBuilder);
    }

    public HttpClient Client { get; }
}
