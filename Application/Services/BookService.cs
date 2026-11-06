using Application.Response;
using Domain.Entities;
using Domain.Interfaces;
using System.Net;

namespace Application.Interfaces;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddBookAsync(Book book)
    {
        var addBook = await _repository.AddBookAsync(book);
        return addBook == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", addBook)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added.", addBook);
    }

    public async Task<ApiResponse<bool>> DeleteBookAsync(int id)
    {
        var deleteBook = await _repository.DeleteBookAsync(id);
        return deleteBook == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", deleteBook)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not deleted!", deleteBook);
    }

    public async Task<ApiResponse<Book>> GetBookAsync(int id)
    {
        var getBook = await _repository.GetBookAsync(id);
        return new ApiResponse<Book>(HttpStatusCode.OK, "Book with id", getBook);
    }

    public async Task<ApiResponse<List<Book>>> GetBooksAsync()
    {
        var listBooks = await _repository.GetBooksAsync();
        return new ApiResponse<List<Book>>(HttpStatusCode.OK, "List of books.", listBooks);
    }

    public async Task<ApiResponse<bool>> UpdateBookAsync(Book book)
    {
        var updateBook = await _repository.UpdateBookAsync(book);
        return updateBook == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Updated!", updateBook)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not udated!", updateBook);
    }
}
