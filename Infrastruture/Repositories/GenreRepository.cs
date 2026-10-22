using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Npgsql;

namespace Infrastruture.Repositories;

public class GenreRepository : IGenreRepository
{
    public void AddGenre(Genre genre)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"insert into genres(name) values(@name)";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("name", genre.Name);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Added!");
    }

    public void DeleteGenre(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "delete from genres where id = @id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Deleted!");
    }

    public Genre GetGenre(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name from genres where id = @id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);
        var reader = cmd.ExecuteReader();
        Genre genre = new();
        while (reader.Read())
        {
            genre.Id = reader.GetInt32(0);
            genre.Name = reader.GetString(1);
        }
        return genre;
    }

    public List<Genre> GetGenres()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name from genres";
        var cmd = new NpgsqlCommand(query, connection);
        var reader = cmd.ExecuteReader();
        List<Genre> genres = [];
        while (reader.Read())
        {
            Genre genre = new();
            genre.Id = reader.GetInt32(0);
            genre.Name = reader.GetString(1);
            genres.Add(genre);
        }
        foreach (var item in genres)
        {
            System.Console.WriteLine(item.Id+" "+item.Name);
        }
        return genres;
    }

    public void UpdateGenre(Genre genre)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "update genres set name = @name where id = @id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("name",genre.Name);
        cmd.Parameters.AddWithValue("id",genre.Id);
        var reader = cmd.ExecuteReader();
        Genre genre1 = new();
        while (reader.Read())
        {
            genre1.Id = genre.Id;
            genre1.Name = genre.Name;
        }
    }
}
