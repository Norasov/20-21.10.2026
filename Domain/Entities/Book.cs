namespace Domain.Entities;

public class Book
{
    public int Id {get;set;}
    public string Title {get;set;}=null!;
    public int PublishedYear {get;set;}
    public int TotalCopies {get;set;}
    public int? AuthorId {get;set;}
    public int? GenreId {get;set;}
}
