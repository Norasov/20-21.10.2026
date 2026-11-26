using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Books;

public class CreateBookDTO
{
    [Required, Length(3, 50)] public string Title { get; set; } = null!;
    [Required, Range(1, 2026)] public int PublishedYear { get; set; }
    [Range(1, int.MaxValue)] public int TotalCopies { get; set; }
    [Range(1, int.MaxValue)] public int? AuthorId { get; set; }
    [Range(1,int.MaxValue)] public int? GenreId { get; set; }
}
