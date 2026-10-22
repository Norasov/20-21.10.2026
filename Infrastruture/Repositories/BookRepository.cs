using System.Data;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Npgsql;

namespace Infrastruture.Repositories;

public class BookRepository : IBookRepository
{
    public void AddBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"insert into books(title, publishedyear,totalcopies,authorid,genreid) values
        (@Title,@PublishedYear,@TotalCopies,@authorid,@genreid)";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("title", book.Title);
        cmd.Parameters.AddWithValue("publishedyear", book.PublishedYear);
        cmd.Parameters.AddWithValue("totalcopies", book.TotalCopies);
        cmd.Parameters.AddWithValue("authorid", book.AuthorId);
        cmd.Parameters.AddWithValue("genreid", book.GenreId);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Added!");
    }

    public void DeleteBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"delete from books where id = @id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Delete!");
    }

    public Book GetBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = $"Select id, title, publishedyear, totalcopies, authorid,genreid from books where id = @id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("id", id);
        var reader = cmd.ExecuteReader();
        Book book = new();
        while (reader.Read())
        {
            book.Id = reader.GetInt32(0);
            book.Title = reader.GetString(1);
            book.PublishedYear = reader.GetInt32(2);
            book.TotalCopies = reader.GetInt32(3);
            book.AuthorId = reader.GetInt32(4);
            book.GenreId = reader.GetInt32(5);
        }
        return book;
    }

    public List<Book> GetBooks()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"select id, title, publishedyear, totalcopies, authorid, genreid from books";
        var cmd = new NpgsqlCommand(query, connection);
        var reader = cmd.ExecuteReader();
        List<Book> books = new();
        while (reader.Read())
        {
            Book book = new();
            book.Id = reader.GetInt32(0);
            book.Title = reader.GetString(1);
            book.PublishedYear = reader.GetInt32(2);
            book.TotalCopies = reader.GetInt32(3);
            book.AuthorId = reader.IsDBNull(4) ? null : reader.GetInt32(4);
            book.GenreId = reader.IsDBNull(5) ? null : reader.GetInt32(5);
            books.Add(book);
        }
        foreach (var item in books)
        {
            System.Console.WriteLine(item.Id + " | " + item.Title + " | " + item.PublishedYear + " | " + item.TotalCopies + " | " + item.AuthorId + " | " + item.GenreId);
        }
        return books;
    }

    public List<Book> GetBooksByAuthor(string fullname)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"select b.id, b.title,b.publishedyear,b.totalcopies
        from books b
        join authors a on b.authorid = a.id
        where a.fullname = @fullname";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("fullname", fullname);
        var reader = cmd.ExecuteReader();
        List<Book> books = [];
        while (reader.Read())
        {
            Book book = new();
            book.Id = reader.GetInt32(0);
            book.Title = reader.GetString(1);
            book.PublishedYear = reader.GetInt32(2);
            book.TotalCopies = reader.GetInt32(3);
            books.Add(book);
        }
        foreach (var item in books)
        {
            System.Console.WriteLine(item.Id + " | " + item.Title + " | " + item.PublishedYear + " | " + item.TotalCopies);
        }
        return books;
    }

    public List<Book> GetBooksByGenre(string name)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @"select b.id, b.title, b.publishedyear,b.totalcopies 
        from books b
        join genres g on b.genreid = g.id
        where g.name = @name";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("name", name);
        var reader = cmd.ExecuteReader();
        List<Book> books = [];
        while (reader.Read())
        {
            Book book = new();
            book.Id = reader.GetInt32(0);
            book.Title = reader.GetString(1);
            book.PublishedYear = reader.GetInt32(2);
            book.TotalCopies = reader.GetInt32(3);
            books.Add(book);
        }
        foreach (var item in books)
        {
            System.Console.WriteLine(item.Id + " | " + item.Title + " | " + item.PublishedYear + " | " + item.TotalCopies);
        }
        return books;
    }

    public void UpdateBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"update books 
        set title = @Title, 
        publishedyear = @PublishedYear, 
        totalcopies = @TotalCopies
        where id =@Id";
        var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("title", book.Title);
        cmd.Parameters.AddWithValue("publishedyear", book.PublishedYear);
        cmd.Parameters.AddWithValue("totalcopies", book.TotalCopies);
        cmd.Parameters.AddWithValue("id", book.Id);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Updated!");
    }
}
