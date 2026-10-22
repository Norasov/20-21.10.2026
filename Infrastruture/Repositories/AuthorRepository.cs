using System.ComponentModel.Design;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Npgsql;

namespace Infrastruture.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public void AddAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"insert into authors(fullname,country,birthyear) values
        (@fullname, @country,@birthyear)";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.Parameters.AddWithValue("fullname",author.FullName);
        cmd.Parameters.AddWithValue("country",author.Country);
        cmd.Parameters.AddWithValue("birthyear",author.BirthYear);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Added!");
    }

    public void DeleteAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = $"delete from authors where id = @id";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.Parameters.AddWithValue("id",id);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Deleted!");
    }

    public Author GetAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = $"select id,fullname,country,birthyear from authors where id = @id";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.Parameters.AddWithValue("id",id);
        var reader = cmd.ExecuteReader();
            Author author = new();
        while (reader.Read())
        {
            author.Id = reader.GetInt32(0);
            author.FullName = reader.GetString(1);
            author.Country = reader.GetString(2);
            author.BirthYear = reader.GetInt32(3);
        }
        return author;
    }

    public List<Author> GetAuthors()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, fullname, country, birthyear from authors";
        var cmd = new NpgsqlCommand(query,connection);
        var reader = cmd.ExecuteReader();
        List<Author> authors = [];
        while (reader.Read())
        {
            Author author = new();
            author.Id = reader.GetInt32(0);
            author.FullName = reader.GetString(1);
            author.Country = reader.GetString(2);
            author.BirthYear = reader.GetInt32(3);
            authors.Add(author);
        }
        foreach (var item in authors)
        {
            System.Console.WriteLine(item.Id+" "+item.FullName+" "+item.Country+" "+item.BirthYear);
        }
        return authors;
    }

    public void UpdateAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"update authors set fullname = @FullName, 
        country = @Country, birthyear = @BirthYear
        where id = @id";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.Parameters.AddWithValue("fullname",author.FullName);
        cmd.Parameters.AddWithValue("country",author.Country);
        cmd.Parameters.AddWithValue("birthyear",author.BirthYear);
        cmd.Parameters.AddWithValue("id",author.Id);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Updated!");
    }

}
