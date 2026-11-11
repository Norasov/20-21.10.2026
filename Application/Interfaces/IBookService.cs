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
}
