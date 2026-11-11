using System.Net;
using Application.DTOs.Genres;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GenreService(IGenreRepository repository) : IGenreService
{
    private readonly IGenreRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddGenreAsync(GenreDTO genreDTO)
    {
        Genre genre = new()
        {
            Name = genreDTO.Name
        };

        var add = await _repository.AddGenreAsync(genre);

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<bool>> DeleteGenreAsync(int id)
    {
        var delete = await _repository.DeleteGenreAsync(id);
        if(!delete)
            return ServiceStatusCodes<bool>.NotFound("Нет жанра с таким идентификатором.");

        return ServiceStatusCodes<bool>.NoContent();
    }

    public async Task<ServiceStatusCodes<GenreDTO>> GetGenreAsync(int id)
    {
        var get = await _repository.GetGenreAsync(id);
        if(get is null)
            return ServiceStatusCodes<GenreDTO>.NotFound("Нет жанров с таким идентификатором.");

        GenreDTO genreDTO = new()
        {
            Name = get.Name
        };

        return ServiceStatusCodes<GenreDTO>.Ok(genreDTO);
    }

    public async Task<ServiceStatusCodes<int>> GetGenresAmountAsync()
    {
        var amount = await _repository.GetGenresAmountAsync();
        return ServiceStatusCodes<int>.Ok(amount);
    }


    public async Task<ServiceStatusCodes<List<GenreDTO>>> GetGenresAsync()
    {
        var genres = await _repository.GetGenresAsync();

        List<GenreDTO> genreDTOs = [];
        foreach (var genre in genres)
        {
            GenreDTO genreDTO = new()
            {
                Name = genre.Name
            };
            genreDTOs.Add(genreDTO);
        }

        return ServiceStatusCodes<List<GenreDTO>>.Ok(genreDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> UpdateGenreAsync(GenreDTO genreDTO, int id)
    {
        var update = await _repository.GetGenreAsync(id);
        if(update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет жанров с таким идентификатором.");

        update.Name = genreDTO.Name;

        var updated = await _repository.UpdateGenreAsync(update);

        return ServiceStatusCodes<bool>.Ok(updated);
    }
}
