using Application.DTOs.Authors;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    private readonly IAuthorRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddAuthorAsync(AuthorDTO authorDTO)
    {
        Author author = new()
        {
            FullName = authorDTO.FullName,
            Country = authorDTO.Country,
            BirthYear = authorDTO.BirthYear,
        };
        var add = await _repository.AddAuthorAsync(author);

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<bool>> DeleteAuthorAsync(int id)
    {
        var delete = await _repository.DeleteAuthorAsync(id);
        if(!delete)
            return ServiceStatusCodes<bool>.NotFound("Нет фвтора с таким идентификатором.");

        return ServiceStatusCodes<bool>.NoContent();
    }

    public async Task<ServiceStatusCodes<AuthorDTO>> GetAuthorAsync(int id)
    {
        var get = await _repository.GetAuthorAsync(id);
        if(get is null)
            ServiceStatusCodes<AuthorDTO>.NotFound("Нет автора с таким идентификатором.");

        AuthorDTO authorDTO = new()
        {
            FullName = get.FullName,
            Country = get.Country,
            BirthYear = get.BirthYear
        };

        return ServiceStatusCodes<AuthorDTO>.Ok(authorDTO);
    }

    public async Task<ServiceStatusCodes<int>> GetAuthorsAmountAsync()
    {
        var amount = await _repository.GetAuthorsAmountAsync();
        return ServiceStatusCodes<int>.Ok(amount);
    }


    public async Task<ServiceStatusCodes<List<AuthorDTO>>> GetAuthorsAsync()
    {
        var authors = await _repository.GetAuthorsAsync();

        List<AuthorDTO> authorDTOs = [];
        foreach (var author in authors)
        {
            AuthorDTO authorDTO = new()
            {
                FullName = author.FullName,
                Country = author.Country,
                BirthYear = author.BirthYear
            };
            authorDTOs.Add(authorDTO);
        }

        return ServiceStatusCodes<List<AuthorDTO>>.Ok(authorDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> UpdateAuthorAsync(AuthorDTO authorDTO, int id)
    {
        var update = await _repository.GetAuthorAsync(id);

        if(update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет автора с таким идентификатором.");


        update.FullName = authorDTO.FullName;
        update.Country = authorDTO.Country;
        update.BirthYear = authorDTO.BirthYear;

        var updated = await _repository.UpdateAuthorAsync(update);
        return ServiceStatusCodes<bool>.Ok(updated);
    }
}
