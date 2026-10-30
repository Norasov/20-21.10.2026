using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;

namespace Infrastruture.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly DapperContext _context;
    public GenreRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task AddGenreAsync(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "insert into genres(name) values(@name)";
        await connection.ExecuteAsync(query, genre);
    }

    public async Task DeleteGenreAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from genres where id = @id";
        await connection.ExecuteAsync(query, new{id});
    }

    public async Task<Genre?> GetGenreAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from genres where id = @id";
        return await connection.QueryFirstOrDefaultAsync<Genre>(query, new{id});
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from genres";
        var list = await connection.QueryAsync<Genre>(query);
        return list.ToList();
    }

    public async Task<int> GetGenresAmountAsync()
    {
        using var connection = _context.Connection();
        var query = "select count(*) from genres";
        return await connection.ExecuteScalarAsync<int>(query);
    }


    public async Task UpdateGenreAsync(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "update genres set name = @name where id = @id";
        await connection.ExecuteAsync(query,genre);
    }
}
