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
        var query = @$"insert into books(title, publishedyear,totalcopies) values
        ('{book.Title}',{book.PublishedYear}, {book.TotalCopies})";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Added!");
    }

    public void DeleteBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"delete from books where id = {id}";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Delete!");
    }

    public Book GetBook(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = $"Select id, title, publishedyear, totalcopies from books where id = {id}";
        var cmd = new NpgsqlCommand(query,connection);
        var reader = cmd.ExecuteReader();
        Book book = new();
        while (reader.Read())
        {
            book.Id = reader.GetInt32(0);
            book.Title = reader.GetString(1);
            book.PublishedYear = reader.GetInt32(2);
            book.TotalCopies = reader.GetInt32(3);
        }
        return book;
    }

    public List<Book> GetBooks()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"select id, title, publishedyear, totalcopies from books";
        var cmd = new NpgsqlCommand(query,connection);
        var reader = cmd.ExecuteReader();
        List<Book> books = new();
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
            System.Console.WriteLine(item.Id+" "+item.Title+" "+item.PublishedYear+" "+item.TotalCopies);
        }
        return books;
    }

    public void UpdateBook(Book book)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = @$"update books 
        set title = '{book.Title}', 
        publishedyear = {book.PublishedYear}, 
        totalcopies = {book.TotalCopies}
        where id = {book.Id}";
        var cmd = new NpgsqlCommand(query,connection);
        cmd.ExecuteNonQuery();
        System.Console.WriteLine("Updated!");
    }
}
