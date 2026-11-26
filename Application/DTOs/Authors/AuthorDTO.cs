using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Authors;

public class AuthorDTO
{
    [Required, MaxLength(100)] public string FullName { get; set; } = null!;
    [Required, MaxLength(50)] public string Country { get; set; } = null!;
    [Required, Range(1900, 2026)] public int BirthYear { get; set; }
}
