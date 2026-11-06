using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService service) : ControllerBase
{
    private readonly IBookService _service = service;
    
    [HttpGet]
    public Task<ApiResponse<List<Book>>> GetBooks()
    {
        return _service.GetBooksAsync();
    }

    [HttpGet("{id:int}")]
    public Task<ApiResponse<Book>> GetBook(int id)
    {
        return _service.GetBookAsync(id);
    }

    [HttpPost]
    public Task<ApiResponse<bool>> AddBook(Book book)
    {
        return _service.AddBookAsync(book);
    }

    [HttpPut]
    public Task<ApiResponse<bool>> UpdateBook(Book book)
    {
        return _service.UpdateBookAsync(book);
    }

    [HttpDelete("{id:int}")]
    public Task<ApiResponse<bool>> DeleteBook(int id)
    {
        return _service.DeleteBookAsync(id);
    }
}
