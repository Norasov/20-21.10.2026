using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
    Task<Author?> GetAuthorAsync(int id);
    Task<List<Author>> GetAuthorsAsync();
    Task<int> GetAuthorsAmountAsync();
}
