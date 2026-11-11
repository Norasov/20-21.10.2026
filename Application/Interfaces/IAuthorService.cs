using Application.Responses;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<ApiResponse<bool>> AddAuthorAsync(Author author);
    Task<ApiResponse<bool>> UpdateAuthorAsync(Author author);
    Task<ApiResponse<bool>> DeleteAuthorAsync(int id);
    Task<ApiResponse<Author>> GetAuthorAsync(int id);
    Task<ApiResponse<List<Author>>> GetAuthorsAsync();
}
