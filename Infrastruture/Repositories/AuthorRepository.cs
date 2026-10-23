using System.Data;
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
        var query = "select * from authors";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable authorsTable = ds.Tables[0];
        DataRow dataRow = authorsTable.NewRow();
        dataRow["fullname"] = author.FullName;
        dataRow["country"] = author.Country;
        dataRow["birthyear"] = author.BirthYear;
        authorsTable.Rows.Add(dataRow);
        adapter.Update(ds);
        System.Console.WriteLine("Added!");
    }

    public void DeleteAuthor(int id)
    {
        using var connection = Database.Connection();
        var query = "select * from authors";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable authorsTable = ds.Tables[0];
        bool f = false;
        foreach (DataRow item in authorsTable.Rows)
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
        else System.Console.WriteLine("author not found");
    }

    public Author GetAuthor(int id)
    {
        using var connection = Database.Connection();
        var query = "select * from authors";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable authorsTable = ds.Tables[0];
        foreach (DataRow item in authorsTable.Rows)
        {
            if (Convert.ToInt32(item["id"]) == id)
            {
                Author author = new();
                author.Id = Convert.ToInt32(item["id"]);
                author.FullName = item["fullname"].ToString()!;
                author.Country = item["country"].ToString()!;
                author.BirthYear = Convert.ToInt32(item["birthyear"]);
                return author;
            }
        }
        throw new Exception("Author not found");
    }

    public List<Author> GetAuthors()
    {
        using var connection = Database.Connection();
        var query = "select * from authors";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable authorsTable = ds.Tables[0];
        List<Author> authors = [];
        foreach (DataRow item in authorsTable.Rows)
        {
            Author author = new();
            author.Id = Convert.ToInt32(item["id"]);
            author.FullName = item["fullname"].ToString()!;
            author.Country = item["country"].ToString()!;
            author.BirthYear = Convert.ToInt32(item["birthyear"]);
            authors.Add(author);
        }
        foreach (var item in authors)
        {
            System.Console.WriteLine(item.Id + " | " + item.FullName + " | " + item.Country + " | " + item.BirthYear);
        }
        return authors;
    }

    public void UpdateAuthor(Author author)
    {
        using var connection = Database.Connection();
        var query = "select * from authors";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable authorsTable = ds.Tables[0];
        bool f = false;
        foreach (DataRow item in authorsTable.Rows)
        {
            if (Convert.ToInt32(item["id"]) == author.Id)
            {
                item["fullname"] = author.FullName;
                item["country"] = author.Country;
                item["birthyear"] = author.BirthYear;
                f = true;
                break;
            }
        }
        if (f)
        {
            adapter.Update(ds);
            System.Console.WriteLine("Updated!");
        }
        else System.Console.WriteLine("Author not found!");
    }

}
