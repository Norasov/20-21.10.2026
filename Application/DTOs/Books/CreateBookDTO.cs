namespace Application.DTOs.Books;

public class CreateBookDTO
{
    public string Title { get; set; } = null!;
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
}
