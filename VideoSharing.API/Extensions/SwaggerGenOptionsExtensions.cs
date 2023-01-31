using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Extensions;

public static class SwaggerGenOptionsExtensions
{
    public static void UseJwtBearerSecurityScheme(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new()
        {
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\".",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Scheme = "Bearer",
            Type = SecuritySchemeType.ApiKey
        });

        options.AddSecurityRequirement(new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new string[] { }
            }
        });
    }
}