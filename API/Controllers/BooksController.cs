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

    [HttpGet("min-published-year-by-author")]
    public async Task<IActionResult> GetMinPublishedYearByAuthor(int id)
    {
        var minYear = await _service.GetMinPublishedYearByAuthorAsync(id);
        return this.ToActionResult(minYear);
    }

    [HttpGet("max-published-year-by-author")]
    public async Task<IActionResult> GetMaxPublishedYearByAuthor(int id)
    {
        var maxYear = await _service.GetMaxPublishedYearByAuthorAsync(id);
        return this.ToActionResult(maxYear);
    }

    [HttpGet("amount-by-published-year/{year}")]
    public async Task<IActionResult> GetAmountByPublishedYear(int year)
    {
        var amount = await _service.GetAmountByPublishedYearAsync(year);
        return this.ToActionResult(amount);
    }

    [HttpGet("amount-by-genre")]
    public async Task<IActionResult> GetAmountByGenre(int id)
    {
        var amount = await _service.GetAmountByGenreAsync(id);
        return this.ToActionResult(amount);
    }

    [HttpGet("min-published-year-by-genre")]
    public async Task<IActionResult> GetMinPublishedYearByGenre(int id)
    {
        var minYear = await _service.GetMinPublishedYearByGenreAsync(id);
        return this.ToActionResult(minYear);
    }

    [HttpGet("max-published-year-by-genre")]
    public async Task<IActionResult> GetMaxPublishedYearByGenre(int id)
    {
        var maxYear = await _service.GetMaxPublishedYearByGenreAsync(id);
        return this.ToActionResult(maxYear);
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