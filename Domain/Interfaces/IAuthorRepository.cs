using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task<bool> AddAuthorAsync(Author author);
    Task<bool> UpdateAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(int id);
    Task<Author?> GetAuthorAsync(int id);
    Task<List<Author>> GetAuthorsAsync();
    Task<int> GetAuthorsAmountAsync();
}
