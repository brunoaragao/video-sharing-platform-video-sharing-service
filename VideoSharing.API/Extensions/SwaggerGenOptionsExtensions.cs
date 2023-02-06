// <copyright file="SwaggerGenOptionsExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Extensions;

/// <summary>
/// Extension methods for <see cref="SwaggerGenOptions"/>.
/// </summary>
public static class SwaggerGenOptionsExtensions
{
    /// <summary>
    /// Adds the JWT Bearer security scheme to the Swagger UI.
    /// </summary>
    /// <param name="options">The <see cref="SwaggerGenOptions"/> to add the security scheme to.</param>
    public static void UseJwtBearerSecurityScheme(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new()
        {
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\".",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Scheme = "Bearer",
            Type = SecuritySchemeType.ApiKey,
        });

        options.AddSecurityRequirement(new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                },
                new string[] { }
            },
        });
    }
}