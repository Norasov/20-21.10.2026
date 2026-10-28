using Domain.Entities;
using Domain.Interfaces;

using Dapper;
using Infrastruture.Data;

namespace Infrastruture.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DapperContext _context;
    public BookRepository(DapperContext context)
    {
        _context = context;
    }
    
    public void AddBook(Book book)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"insert into books(title, publishedyear, totalcopies, authorid, genreid) values
        (@title, @publishedyear, @totalcopies, @authorid, @genreid)";
        connection.Execute(query, book);
    }

    public void DeleteBook(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from books where id = @id";
        connection.Execute(query, new{id});
    }

    public int GetAmountByGenre(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select count(genreid) from books
        where genreid = @genreid";
        return connection.ExecuteScalar<int>(query, new{genreId});
    }

    public int GetAmountByPublishedYear(int year)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select count(*) from books
        where publishedyear = @publishedyear";
        return connection.ExecuteScalar<int>(query, new{publishedyear = year});
    }

    public Book? GetBook(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from books where id = @id";
        var book = connection.QueryFirstOrDefault<Book>(query,new{id});
        return book;
    }

    public List<Book> GetBooks()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from books";
        return connection.Query<Book>(query).ToList();
    }

    public int GetBooksAmount()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select count(*) from books";
        return connection.ExecuteScalar<int>(query);
    }


    public List<Book> GetBooksByAuthor(string fullname)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select b.id, b.title,b.publishedyear,b.totalcopies
        from books b
        join authors a on b.authorid = a.id
        where lower(a.fullname) =lower(@fullname)";
        return connection.Query<Book>(query,new{fullname}).ToList();
    }

    public List<Book> GetBooksByGenre(string name)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select b.id, b.title, b.publishedyear,b.totalcopies 
        from books b
        join genres g on b.genreid = g.id
        where lower(g.name) = lower(@name)";
        return connection.Query<Book>(query,new{name}).ToList();
    }

    public int GetMaxPublishedYearByAuthor(int authorId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select max(publishedyear) from books
        where authorid = @authorid";
        return connection.ExecuteScalar<int>(query, new{authorId});
    }

    public int GetMinPublishedYearByAuthor(int authorId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select min(publishedyear) from books
        where authorid = @authorid";
        return connection.ExecuteScalar<int>(query, new{authorId});
    }
    public int GetMaxPublishedYearByGenre(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select max(publishedyear) from books
        where genreid = @genreid";
        return connection.ExecuteScalar<int>(query, new{genreId});
    }

    public int GetMinPublishedYearByGenre(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select min(publishedyear) from books
        where genreid = @genreid";
        return connection.ExecuteScalar<int>(query, new{genreId});
    }

    public void UpdateBook(Book book)
    {
        using var connection = _context.Connection();
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
