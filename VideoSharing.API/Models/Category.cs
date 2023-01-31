using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models;

public class Category
{
    public const int DefaultCategoryId = 1;

    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [StringLength(7, MinimumLength = 7)]
    [RegularExpression("^#([A-Fa-f0-9]{6})$")]
    public required string Color { get; set; }

    public void Deconstruct(out int id, out string name, out string color)
    {
        id = Id;
        name = Name;
        color = Color;
    }
}
