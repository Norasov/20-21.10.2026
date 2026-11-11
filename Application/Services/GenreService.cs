using System.Net;
using Application.Interfaces;
using Application.Responses;
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
        ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", addGenre)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Added!", false);
    }

    public async Task<ApiResponse<bool>> DeleteGenreAsync(int id)
    {
        var deleteGenre = await _repository.DeleteGenreAsync(id);
        return deleteGenre == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", deleteGenre)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Not deleted!", false);
    }

    public async Task<ApiResponse<Genre>> GetGenreAsync(int id)
    {
        var getGenre = await _repository.GetGenreAsync(id);
        return getGenre != null
        ? new ApiResponse<Genre>(HttpStatusCode.OK, "Genre with id", getGenre)
        : new ApiResponse<Genre>(HttpStatusCode.NotFound, "Genre not found");
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
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Genre not found!", false);
    }
}
