using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenreRepository
{
    void AddGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(int id);
    Genre GetGenre(int id);
    List<Genre> GetGenres();
}
