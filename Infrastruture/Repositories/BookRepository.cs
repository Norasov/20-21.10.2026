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
        var query = "select * from books";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable booksTable = ds.Tables[0];
        DataRow dataRow = booksTable.NewRow();
        book.Title = dataRow["title"].ToString()!;
        book.PublishedYear = Convert.ToInt32(dataRow["publishedyear"]);
        book.TotalCopies = Convert.ToInt32(dataRow["totalcopies"]);
        book.AuthorId = Convert.ToInt32(dataRow["authorid"]);
        book.GenreId = Convert.ToInt32(dataRow["genreid"]);
        adapter.Update(ds);
        System.Console.WriteLine("Added!");
    }

    public void DeleteBook(int id)
    {
        using var connection = Database.Connection();
        var query = "select * from books";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable booksTable = ds.Tables[0];
        bool f = false;
        foreach (DataRow item in booksTable.Rows)
        {
            if (Convert.ToInt32(item["id"]) == id)
            {
                item.Delete();
                f = true;
                break;
            }
        }
        if (f)
        {
            adapter.Update(ds);
            System.Console.WriteLine("Deleted!");
        }
        else System.Console.WriteLine("Book not found");
    }

    public Book GetBook(int id)
    {
        using var connection = Database.Connection();
        var query = "select * from books where id = @id";
        NpgsqlDataAdapter adapter = new(query, connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@id", id);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable booksTable = ds.Tables[0];
        if (booksTable.Rows.Count == 0)
        {
            throw new Exception("Book not found");
        }
        DataRow dataRow = booksTable.Rows[0];
        Book book = new();
        book.Id = Convert.ToInt32(dataRow["id"]);
        book.Title = dataRow["title"].ToString()!;
        book.PublishedYear = Convert.ToInt32(dataRow["publishedyear"]);
        book.TotalCopies = Convert.ToInt32(dataRow["totalcopies"]);
        return book;
    }

    public List<Book> GetBooks()
    {
        using var connection = Database.Connection();
        var query = "select * from books";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable booksTable = ds.Tables[0];
        List<Book> books = [];
        foreach (DataRow item in booksTable.Rows)
        {
            Book book = new();
            book.Id = Convert.ToInt32(item["id"]);
            book.Title = item["title"].ToString()!;
            book.PublishedYear = Convert.ToInt32(item["publishedyear"]);
            book.TotalCopies = Convert.ToInt32(item["totalcopies"]);
            books.Add(book);
        }
        foreach (var item in books)
        {
            System.Console.WriteLine(item.Id + " | " + item.Title + " | " + item.PublishedYear + " | " + item.TotalCopies);
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
        NpgsqlDataAdapter adapter = new(query, connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@fullname", fullname);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable booksTable = ds.Tables[0];
        List<Book> books = [];
        foreach (DataRow item in booksTable.Rows)
        {
            Book book = new();
            book.Id = Convert.ToInt32(item["id"]);
            book.Title = item["title"].ToString()!;
            book.PublishedYear = Convert.ToInt32(item["publishedyear"]);
            book.TotalCopies = Convert.ToInt32(item["totalcopies"]);
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
        var query = @"select b.id, b.title, b.publishedyear,b.totalcopies 
        from books b
        join genres g on b.genreid = g.id
        where g.name = @name";
        NpgsqlDataAdapter adapter = new(query, connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@name",name);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable booksTable = ds.Tables[0];
        List<Book> books = [];
        foreach (DataRow item in booksTable.Rows)
        {
            Book book = new();
            book.Id = Convert.ToInt32(item["id"]);
            book.Title = item["title"].ToString()!;
            book.PublishedYear = Convert.ToInt32(item["publishedyear"]);
            book.TotalCopies = Convert.ToInt32(item["totalcopies"]);
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
        var query = "select * from books";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable booksTable = ds.Tables[0];
        bool f = false;
        foreach (DataRow item in booksTable.Rows)
        {
            if (Convert.ToInt32(item["id"]) == book.Id)
            {
                item["title"] = book.Title;
                item["publishedyear"] = book.PublishedYear;
                item["totalcopies"] = book.TotalCopies;
                f = true;
                break;
            }
        }
        if (f)
        {
            adapter.Update(ds);
            System.Console.WriteLine("Updated!");
        }
        else System.Console.WriteLine("Book Bot found");
    }
}
