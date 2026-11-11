using Application.HttpStatusCodes;
using Application.DTOs.Authors;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<ServiceStatusCodes<bool>> AddAuthorAsync(AuthorDTO authorDTO);
    Task<ServiceStatusCodes<bool>> UpdateAuthorAsync(AuthorDTO authorDTO, int id);
    Task<ServiceStatusCodes<bool>> DeleteAuthorAsync(int id);
    Task<ServiceStatusCodes<AuthorDTO>> GetAuthorAsync(int id);
    Task<ServiceStatusCodes<List<AuthorDTO>>> GetAuthorsAsync();
    Task<ServiceStatusCodes<int>> GetAuthorsAmountAsync();
}
