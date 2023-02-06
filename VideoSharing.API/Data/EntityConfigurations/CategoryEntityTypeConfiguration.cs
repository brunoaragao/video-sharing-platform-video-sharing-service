// <copyright file="CategoryEntityTypeConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data.EntityConfigurations;

/// <summary>
/// Configures the <see cref="Category"/> entity.
/// </summary>
public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");

        builder.HasKey(c => c.Id)
            .HasName("pk_category");

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Color)
            .HasColumnName("color")
            .IsRequired()
            .HasMaxLength(7)
            .IsFixedLength();

        builder.HasData(
            new Category { Id = 1, Name = "Default", Color = "#000000" });
    }
}