using API.Extensions;
using Application.DTOs.Genres;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(
    IGenreService service,
    ILogger<GenresController> logger) : ControllerBase
{
    private IGenreService _service = service;
    private readonly ILogger<GenresController> _logger = logger;
    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _service.GetGenresAsync();
        return this.ToActionResult(genres);
    }

    [HttpGet("amount-of-genres")]
    public async Task<IActionResult> AmountOfGenres()
    {
        var amount = await _service.GetGenresAmountAsync();
        return this.ToActionResult(amount);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _service.GetGenreAsync(id);
        return this.ToActionResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre(GenreDTO genreDTO)
    {
        var add = await _service.AddGenreAsync(genreDTO);
        return this.ToActionResult(add);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGenre(GenreDTO genreDTO, int id)
    {
        var update = await _service.UpdateGenreAsync(genreDTO, id);
        return this.ToActionResult(update);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var delete = await _service.DeleteGenreAsync(id);
        return this.ToActionResult(delete);
    }
}
