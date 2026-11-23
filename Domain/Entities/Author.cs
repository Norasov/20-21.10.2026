using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Author
{
    [Range(1, int.MaxValue)] public int Id {get;set;}
    [Required, MaxLength(100)] public string FullName {get;set;}=null!;
    [Required, MaxLength(50)] public string Country {get;set;}=null!;
    [Required, Range(1900, 2026)] public int BirthYear {get;set;}

    public List<Book> Books {get;set;}= [];
}
