using System.Net;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    private readonly IAuthorRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddAuthorAsync(Author author)
    {
        var addAuthor = await _repository.AddAuthorAsync(author);
        return addAuthor == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", addAuthor)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Added!", addAuthor);
    }

    public async Task<ApiResponse<bool>> DeleteAuthorAsync(int id)
    {
        var deleteAuthor = await _repository.DeleteAuthorAsync(id);
        return deleteAuthor == true
        ? new ApiResponse<bool>(HttpStatusCode.OK,"Deleted!", deleteAuthor)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not Deleted!", deleteAuthor);
    }

    public async Task<ApiResponse<Author>> GetAuthorAsync(int id)
    {
        var getAuthor = await _repository.GetAuthorAsync(id);
        return new ApiResponse<Author>(HttpStatusCode.OK, "Author with id", getAuthor);
    }

    public async Task<ApiResponse<List<Author>>> GetAuthorsAsync()
    {
        var listAuthors = await _repository.GetAuthorsAsync();
        return new ApiResponse<List<Author>>(HttpStatusCode.OK, "list of Authors", listAuthors);
    }

    public async Task<ApiResponse<bool>> UpdateAuthorAsync(Author author)
    {
        var updateAuthor = await _repository.UpdateAuthorAsync(author);
        return updateAuthor == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Updated!", updateAuthor)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not updated!", updateAuthor);
    }
}
