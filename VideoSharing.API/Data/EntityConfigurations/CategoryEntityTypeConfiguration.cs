using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data.EntityConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
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
            new Category { Id = 1, Name = "Default", Color = "#000000" }
        );
    }
}
