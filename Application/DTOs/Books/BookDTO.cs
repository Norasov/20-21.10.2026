namespace Application.DTOs.Books;

public class BookDTO
{
    public string Title { get; set; } = null!;
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }

}
