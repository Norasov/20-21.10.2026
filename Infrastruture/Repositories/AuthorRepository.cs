using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;

namespace Infrastruture.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public void AddAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"insert into authors(fullname, country, birthyear) values
        (@fullname, @country, @birthyear)";
        connection.Execute(query,author);
    }

    public void DeleteAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "delete from authors where id = @id";
        connection.Execute(query, new{id});
    }

    public Author? GetAuthor(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select * from authors where id = @id";
        return connection.QueryFirstOrDefault<Author>(query,new{id});
    }

    public List<Author> GetAuthors()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select * from authors";
        return connection.Query<Author>(query).ToList();
    }

    public void UpdateAuthor(Author author)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "update authors set fullname = @fullname, country= @country, birthyear = @birthyear where id = @id";
        connection.Execute(query, author);
    }
}
