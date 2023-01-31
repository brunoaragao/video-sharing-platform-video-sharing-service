using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Data.EntityConfigurations;

public class VideoEntityTypeConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("video");

        builder.HasKey(v => v.Id)
            .HasName("pk_video");

        builder.Property(v => v.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(v => v.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(2_000);

        builder.Property(v => v.Url)
            .HasColumnName("url")
            .IsRequired()
            .HasMaxLength(2_048);

        builder.Property(v => v.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(v => v.CategoryId)
            .HasConstraintName("fk_video_category")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => v.CategoryId)
            .HasDatabaseName("ix_video_category_id");
    }
}
