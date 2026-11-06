using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenreRepository
{
    Task<bool> AddGenreAsync(Genre genre);
    Task<bool> UpdateGenreAsync(Genre genre);
    Task<bool> DeleteGenreAsync(int id);
    Task<Genre?> GetGenreAsync(int id);
    Task<List<Genre>> GetGenresAsync();
    Task<int> GetGenresAmountAsync();
}
