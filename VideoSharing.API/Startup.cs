// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text;

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API;

/// <summary>
/// Represents the startup class.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<VideoSharingContext>(options =>
        {
            options.UseNpgsql("Name=VideoSharingConnection");
        });

        services.AddScoped<VideoSharingContextSeed>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"]!);

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = this.Configuration["Jwt:Issuer"],
                    ValidAudience = this.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "VideoSharing API",
                Version = "v1",
            });

            options.UseJwtBearerSecurityScheme();
        });
    }

    /// <summary>
    /// Configures the application.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="env">The environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "VideoSharing API v1");
            });
        }

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .MapControllers()
                .RequireAuthorization();
        });
    }
}