using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models.RatingEntities;

public abstract class Rating
{
    [Key]
    public string? Login { get; set; }
    public string? Name { get; set; }
    public int? Games { get; set; }
}
