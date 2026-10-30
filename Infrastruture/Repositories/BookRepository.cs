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

    public async Task AddBookAsync(Book book)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"insert into books(title, publishedyear, totalcopies, authorid, genreid) values
        (@title, @publishedyear, @totalcopies, @authorid, @genreid)";
        await connection.ExecuteAsync(query, book);
    }

    public async Task DeleteBookAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "delete from books where id = @id";
        await connection.ExecuteAsync(query, new { id });
    }

    public async Task<int> GetAmountByGenreAsync(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select count(genreid) from books
        where genreid = @genreid";
        return await connection.ExecuteScalarAsync<int>(query, new { genreId });
    }

    public async Task<int> GetAmountByPublishedYearAsync(int year)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select count(*) from books
        where publishedyear = @publishedyear";
        return await connection.ExecuteScalarAsync<int>(query, new { publishedyear = year });
    }


    public async Task<List<Book>> GetBooksAsync()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from books";
        var list = await connection.QueryAsync<Book>(query);
        return list.ToList();
    }

    public async Task<int> GetBooksAmountAsync()
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select count(*) from books";
        return await connection.ExecuteScalarAsync<int>(query);
    }


    public async Task<List<Book>> GetBooksByAuthorAsync(string fullname)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select b.id, b.title,b.publishedyear,b.totalcopies
        from books b
        join authors a on b.authorid = a.id
        where lower(a.fullname) =lower(@fullname)";
        var list = await connection.QueryAsync<Book>(query, new { fullname });
        return list.ToList();
    }

    public async Task<List<Book>> GetBooksByGenreAsync(string name)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select b.id, b.title, b.publishedyear,b.totalcopies 
        from books b
        join genres g on b.genreid = g.id
        where lower(g.name) = lower(@name)";
        var list = await connection.QueryAsync<Book>(query, new { name });
        return list.ToList();
    }

    public async Task<int> GetMaxPublishedYearByAuthorAsync(int authorId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select max(publishedyear) from books
        where authorid = @authorid";
        return await connection.ExecuteScalarAsync<int>(query, new { authorId });
    }

    public async Task<int> GetMinPublishedYearByAuthorAsync(int authorId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select min(publishedyear) from books
        where authorid = @authorid";
        return await connection.ExecuteScalarAsync<int>(query, new { authorId });
    }
    public async Task<int> GetMaxPublishedYearByGenreAsync(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select max(publishedyear) from books
        where genreid = @genreid";
        return await connection.ExecuteScalarAsync<int>(query, new { genreId });
    }

    public async Task<int> GetMinPublishedYearByGenreAsync(int genreId)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"select min(publishedyear) from books
        where genreid = @genreid";
        return await connection.ExecuteScalarAsync<int>(query, new { genreId });
    }

    public async Task UpdateBookAsync(Book book)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = @"update books set title = @title, 
        publishedyear = @publishedyear, 
        totalcopies = @totalcopies,
        authorid = @authorid,
        genreid = @genreid
        where id = @id";
        await connection.ExecuteAsync(query, book);
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        using var connection = _context.Connection();
        connection.Open();
        var query = "select * from books where id = @id";
        var book = await connection.QueryFirstOrDefaultAsync<Book>(query, new { id });
        return book;
    }
}
