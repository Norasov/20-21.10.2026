using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGenreService
{
    Task<ApiResponse<bool>> AddGenreAsync(Genre genre);
    Task<ApiResponse<bool>> UpdateGenreAsync(Genre genre);
    Task<ApiResponse<bool>> DeleteGenreAsync(int id);
    Task<ApiResponse<Genre>> GetGenreAsync(int id);
    Task<ApiResponse<List<Genre>>> GetGenresAsync();
}
