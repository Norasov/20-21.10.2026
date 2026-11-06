using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;

namespace Infrastruture.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;
    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddGenreAsync(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "insert into genres(name) values(@name)";
        var addGenre = await connection.ExecuteAsync(query, genre);
        return addGenre > 0;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from genres where id = @id";
        var deleteGenre = await connection.ExecuteAsync(query, new{id});
        return deleteGenre > 0;
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


    public async Task<bool> UpdateGenreAsync(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "update genres set name = @name where id = @id";
        var updateGenre = await connection.ExecuteAsync(query,genre);
        return updateGenre > 0;
    }
}
