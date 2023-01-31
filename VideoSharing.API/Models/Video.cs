using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

public class Video
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Title { get; set; }

    [Required]
    [StringLength(2_000, MinimumLength = 10)]
    public required string Description { get; set; }

    [Required]
    [StringLength(2_048, MinimumLength = 10)]
    [Url]
    public required string Url { get; set; }

    [Range(1, int.MaxValue)]
    [DefaultValue(Category.DefaultCategoryId)]
    public int CategoryId { get; set; } = Category.DefaultCategoryId;

    public void Deconstruct(out int id, out string title, out string description, out string url, out int categoryId)
    {
        id = Id;
        title = Title;
        description = Description;
        url = Url;
        categoryId = CategoryId;
    }
}
