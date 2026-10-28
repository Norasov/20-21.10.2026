using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository
{
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
    Book? GetBook(int id);
    List<Book> GetBooks();
    List<Book> GetBooksByAuthor(string fullname);
    List<Book> GetBooksByGenre(string name);
    int GetBooksAmount();
    int GetMinPublishedYearByAuthor(int authorId);
    int GetMaxPublishedYearByAuthor(int authorId);
    int GetAmountByPublishedYear(int year);
    int GetAmountByGenre(int genreId);
    int GetMinPublishedYearByGenre(int genreId);
    int GetMaxPublishedYearByGenre(int genreId);
}
