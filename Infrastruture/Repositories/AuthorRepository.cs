using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;

namespace Infrastruture.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DapperContext _context;
    public AuthorRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task AddAuthorAsync(Author author)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"insert into authors(fullname, country, birthyear) values
        (@fullname, @country, @birthyear)";
        await connection.ExecuteAsync(query,author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from authors where id = @id";
        await connection.ExecuteAsync(query, new{id});
    }

    public async Task<Author?> GetAuthorAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from authors where id = @id";
        return await connection.QueryFirstOrDefaultAsync<Author>(query,new{id});
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from authors";
        var list = await connection.QueryAsync<Author>(query);
        return list.ToList();
    }

    public async Task<int> GetAuthorsAmountAsync()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select count(*) from authors";
        return await connection.ExecuteScalarAsync<int>(query);
    }


    public async Task UpdateAuthorAsync(Author author)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "update authors set fullname = @fullname, country= @country, birthyear = @birthyear where id = @id";
        await connection.ExecuteAsync(query, author);
    }
}
