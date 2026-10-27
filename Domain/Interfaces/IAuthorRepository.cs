using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    void AddAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(int id);
    Author? GetAuthor(int id);
    List<Author> GetAuthors();
}
