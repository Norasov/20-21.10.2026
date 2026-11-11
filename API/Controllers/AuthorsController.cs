using API.Extensions;
using Application.DTOs.Authors;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService service) : ControllerBase
{
    private readonly IAuthorService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _service.GetAuthorsAsync();
        return this.ToActionResult(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _service.GetAuthorAsync(id);
        return this.ToActionResult(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(AuthorDTO authorDTO)
    {
        var add = await _service.AddAuthorAsync(authorDTO);
        return this.ToActionResult(add);
    }

    [HttpPut] 
    public async Task<IActionResult> UpdateAuthor(AuthorDTO authorDTO, int id)
    {
        var update = await _service.UpdateAuthorAsync(authorDTO, id);
        return this.ToActionResult(update);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var delete = await _service.DeleteAuthorAsync(id);
        return this.ToActionResult(delete);
    }

    [HttpGet("amount-of-authors")]
    public async Task<IActionResult> AmountOfAuthors()
    {
        var amount = await _service.GetAuthorsAmountAsync();
        return this.ToActionResult(amount);
    }
}
