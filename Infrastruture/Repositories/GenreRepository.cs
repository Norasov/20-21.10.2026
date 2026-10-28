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
    public void AddGenre(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "insert into genres(name) values(@name)";
        connection.Execute(query, genre);
    }

    public void DeleteGenre(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from genres where id = @id";
        connection.Execute(query, new{id});
    }

    public Genre? GetGenre(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from genres where id = @id";
        return connection.QueryFirstOrDefault<Genre>(query, new{id});
    }

    public List<Genre> GetGenres()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from genres";
        return connection.Query<Genre>(query).ToList();
    }

    public int GetGenresAmount()
    {
        using var connection = _context.Connection();
        var query = "select count(*) from genres";
        return connection.ExecuteScalar<int>(query);
    }


    public void UpdateGenre(Genre genre)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "update genres set name = @name where id = @id";
        connection.Execute(query,genre);
    }
}
