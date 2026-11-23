using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Book
{
    [Range(1, int.MaxValue)] public int Id {get;set;}
    [Required, Length(3, 50)] public string Title {get;set;}=null!;
    [Required, Range(1, 2026)] public int PublishedYear {get;set;}
    [Range(1, int.MaxValue)] public int TotalCopies {get;set;}
    
    [Range(1, int.MaxValue)] public int? AuthorId {get;set;}
    public Author Author {get;set;} = null!;

    [Range(1,int.MaxValue)] public int? GenreId {get;set;}
    public Genre Genre{get;set;}=null!;
}
