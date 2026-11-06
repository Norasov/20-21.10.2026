using System.Net;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GenreService(IGenreRepository repository) : IGenreService
{
    private readonly IGenreRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddGenreAsync(Genre genre)
    {
        var addGenre = await _repository.AddGenreAsync(genre);
        return addGenre == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", addGenre)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Added!", addGenre);
    }

    public async Task<ApiResponse<bool>> DeleteGenreAsync(int id)
    {
        var deleteGenre = await _repository.DeleteGenreAsync(id);
        return deleteGenre == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", deleteGenre)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not deleted!", deleteGenre);
    }

    public async Task<ApiResponse<Genre>> GetGenreAsync(int id)
    {
        var getGenre = await _repository.GetGenreAsync(id);
        return new ApiResponse<Genre>(HttpStatusCode.OK, "Genre with id", getGenre);
    }

    public async Task<ApiResponse<List<Genre>>> GetGenresAsync()
    {
        var listGenre = await _repository.GetGenresAsync();
        return new ApiResponse<List<Genre>>(HttpStatusCode.OK,"List of genres", listGenre);
    }

    public async Task<ApiResponse<bool>> UpdateGenreAsync(Genre genre)
    {
        var updateGenre = await _repository.UpdateGenreAsync(genre);
        return updateGenre == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Updated!", updateGenre)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not updated!", updateGenre);
    }
}
