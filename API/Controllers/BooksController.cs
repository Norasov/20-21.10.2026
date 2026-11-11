using API.Extensions;
using Application.DTOs.Books;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService service) : ControllerBase
{
    private readonly IBookService _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _service.GetBooksAsync();
        return this.ToActionResult(books);
    }

    [HttpGet("amount-of-books")]
    public async Task<IActionResult> AmountOfBooks()
    {
        var amount = await _service.GetBooksAmountAsync();
        return this.ToActionResult(amount);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _service.GetBookAsync(id);
        return this.ToActionResult(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(CreateBookDTO bookDTO)
    {
        var add = await _service.AddBookAsync(bookDTO);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(UpdateBookDTO bookDTO, int id)
    {
        var update = await _service.UpdateBookAsync(bookDTO, id);
        return this.ToActionResult(update);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var delete =await _service.DeleteBookAsync(id);
        return this.ToActionResult(delete);
    }
}
