using Application.DTOs.Books;
using Application.HttpStatusCodes;

namespace Application.Interfaces;

public interface IBookService
{
    Task<ServiceStatusCodes<bool>> AddBookAsync(CreateBookDTO bookDTO);
    Task<ServiceStatusCodes<bool>> UpdateBookAsync(UpdateBookDTO bookDTO, int id);
    Task<ServiceStatusCodes<bool>> DeleteBookAsync(int id);
    Task<ServiceStatusCodes<BookDTO>> GetBookAsync(int id);
    Task<ServiceStatusCodes<List<BookDTO>>> GetBooksAsync();
    Task<ServiceStatusCodes<int>> GetBooksAmountAsync();
    Task<ServiceStatusCodes<int>> GetMinPublishedYearByAuthorAsync(int authorId);
    Task<ServiceStatusCodes<int>> GetMaxPublishedYearByAuthorAsync(int authorId);
    Task<ServiceStatusCodes<int>> GetAmountByPublishedYearAsync(int year);
    Task<ServiceStatusCodes<int>> GetAmountByGenreAsync(int genreId);
    Task<ServiceStatusCodes<int>> GetMinPublishedYearByGenreAsync(int genreId);
    Task<ServiceStatusCodes<int>> GetMaxPublishedYearByGenreAsync(int genreId);
}
