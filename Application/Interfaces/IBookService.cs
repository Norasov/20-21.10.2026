using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBookService
{
    Task<ApiResponse<bool>> AddBookAsync(Book book);
    Task<ApiResponse<bool>> UpdateBookAsync(Book book);
    Task<ApiResponse<bool>> DeleteBookAsync(int id);
    Task<ApiResponse<Book>> GetBookAsync(int id);
    Task<ApiResponse<List<Book>>> GetBooksAsync();
}
