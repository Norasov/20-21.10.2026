using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Dapper;

namespace Infrastruture.Repositories;

public class BookRepository : IBookRepository
{
    public void AddBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"insert into books(title, publishedyear, totalcopies, authorid, genreid) values
        (@title, @publishedyear, @totalcopies, @authorid, @genreid)";
        connection.Execute(query, book);
    }

    public void DeleteBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "delete from books where id = @id";
        connection.Execute(query, new{id});
    }

    public Book? GetBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select * from books where id = @id";
        var book = connection.QueryFirstOrDefault<Book>(query,new{id});
        return book;
    }

    public List<Book> GetBooks()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select * from books";
        return connection.Query<Book>(query).ToList();
    }

    public List<Book> GetBooksByAuthor(string fullname)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"select b.id, b.title,b.publishedyear,b.totalcopies
        from books b
        join authors a on b.authorid = a.id
        where a.fullname = @fullname";
        return connection.Query<Book>(query,new{fullname}).ToList();
    }

    public List<Book> GetBooksByGenre(string name)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"select b.id, b.title, b.publishedyear,b.totalcopies 
        from books b
        join genres g on b.genreid = g.id
        where g.name = @name";
        return connection.Query<Book>(query,new{name}).ToList();
    }

    public void UpdateBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"update books set title = @title, 
        publishedyear = @publishedyear, 
        totalcopies = @totalcopies,
        authorid = @authorid,
        genreid = @genreid
        where id = @id";
        connection.Execute(query,book);
    }
}
