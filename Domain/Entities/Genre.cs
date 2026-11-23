using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Genre
{
    public int Id { get; set; }
    [Required, Length(3, 50)] public string Name { get; set; } = null!;

    public List<Book> Books {get;set;}= [];
}
