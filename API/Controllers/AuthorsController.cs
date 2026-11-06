using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService service) : ControllerBase
{
    private readonly IAuthorService _service = service;

    [HttpGet]
    public Task<ApiResponse<List<Author>>> GetAuthors()
    {
        return _service.GetAuthorsAsync();
    }

    [HttpGet("{id:int}")]
    public Task<ApiResponse<Author>> GetAuthor(int id)
    {
        return _service.GetAuthorAsync(id);
    }

    [HttpPost]
    public Task<ApiResponse<bool>> AddAuthor(Author author)
    {
        return _service.AddAuthorAsync(author);
    }

    [HttpPut] 
    public Task<ApiResponse<bool>> UpdateAuthor(Author author)
    {
        return _service.UpdateAuthorAsync(author);
    }

    [HttpDelete("{id:int}")]
    public Task<ApiResponse<bool>> DeleteAuthor(int id)
    {
        return _service.DeleteAuthorAsync(id);
    }
}
