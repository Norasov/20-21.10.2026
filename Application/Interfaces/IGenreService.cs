using Application.DTOs.Genres;
using Application.HttpStatusCodes;

namespace Application.Interfaces;

public interface IGenreService
{
    Task<ServiceStatusCodes<bool>> AddGenreAsync(GenreDTO genreDTO);
    Task<ServiceStatusCodes<bool>> UpdateGenreAsync(GenreDTO genreDTO, int id);
    Task<ServiceStatusCodes<bool>> DeleteGenreAsync(int id);
    Task<ServiceStatusCodes<GenreDTO>> GetGenreAsync(int id);
    Task<ServiceStatusCodes<List<GenreDTO>>> GetGenresAsync();
    Task<ServiceStatusCodes<int>> GetGenresAmountAsync();
}
