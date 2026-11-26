using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Genres;

public class GenreDTO
{
    [Required, Length(3, 50)] public string Name { get; set; } = null!;
}
