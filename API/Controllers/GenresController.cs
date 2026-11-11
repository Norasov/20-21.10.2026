using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(IGenreService service) : ControllerBase
{
    private IGenreService _service = service;

    [HttpGet]
    public Task<ApiResponse<List<Genre>>> GetGenres()
    {
        return _service.GetGenresAsync();
    }
    
    [HttpGet("{id:int}")]
    public Task<ApiResponse<Genre>> GetGenre(int id)
    {
        return _service.GetGenreAsync(id);
    }

    [HttpPost]
    public Task<ApiResponse<bool>> AddGenre(Genre genre)
    {
        return _service.AddGenreAsync(genre);
    }

    [HttpPut]
    public Task<ApiResponse<bool>> UpdateGenre(Genre genre)
    {
        return _service.UpdateGenreAsync(genre);
    }

    [HttpDelete("{id:int}")]
    public Task<ApiResponse<bool>> DeleteGenre(int id)
    {
        return _service.DeleteGenreAsync(id);
    }
}
