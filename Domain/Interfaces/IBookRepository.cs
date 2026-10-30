using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<Book?> GetBookAsync(int id);
    Task<List<Book>> GetBooksAsync();
    Task<List<Book>> GetBooksByAuthorAsync(string fullname);
    Task<List<Book>> GetBooksByGenreAsync(string name);
    Task<int> GetBooksAmountAsync();
    Task<int> GetMinPublishedYearByAuthorAsync(int authorId);
    Task<int> GetMaxPublishedYearByAuthorAsync(int authorId);
    Task<int> GetAmountByPublishedYearAsync(int year);
    Task<int> GetAmountByGenreAsync(int genreId);
    Task<int> GetMinPublishedYearByGenreAsync(int genreId);
    Task<int> GetMaxPublishedYearByGenreAsync(int genreId);
}
