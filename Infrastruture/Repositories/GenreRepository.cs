using System.Data;
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
        var sql = "select * from genres";
        NpgsqlDataAdapter adapter = new(sql, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable genresTable = ds.Tables[0];
        DataRow row = genresTable.NewRow();
        row["name"] = genre.Name;
        genresTable.Rows.Add(row);
        adapter.Update(ds);
        System.Console.WriteLine("Added!");
    }

    public void DeleteGenre(int id)
    {
        using var connection = Database.Connection();
        var sql = "select * from genres";
        NpgsqlDataAdapter adapter = new(sql, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable genresTable = ds.Tables[0];
        bool deleted = false;
        foreach (DataRow dataRow in genresTable.Rows)
        {
            if (Convert.ToInt32(dataRow["id"]) == id)
            {
                dataRow.Delete();
                deleted = true;
                break;
            }
        }
        if (deleted)
        {
            adapter.Update(ds);
            System.Console.WriteLine("Deleted!");
        }
        else
        {
            System.Console.WriteLine("Genre not found!");
        }
    }

    public Genre GetGenre(int id)
    {
        using var connection = Database.Connection();
        var query = "select * from genres where id = @id";

        NpgsqlDataAdapter adapter = new(query, connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@id", id);
        DataSet ds = new();
        adapter.Fill(ds);
        DataTable genresTable = ds.Tables[0];
        if (genresTable.Rows.Count == 0)
        {
            throw new Exception("Genre not found");
        }

        DataRow item = genresTable.Rows[0];

        Genre genre = new();
        genre.Id = Convert.ToInt32(item["id"]);
        genre.Name = item["name"].ToString()!;
        return genre;
    }

    public List<Genre> GetGenres()
    {
        using var connection = Database.Connection();
        var sql = "select id, name from genres";
        var adapter = new NpgsqlDataAdapter(sql, connection);
        var ds = new DataSet();
        adapter.Fill(ds);
        DataTable genresTable = ds.Tables[0];

        List<Genre> genres = [];

        foreach (DataRow item in genresTable.Rows)
        {
            Genre genre = new();
            genre.Id = Convert.ToInt32(item[0]);
            genre.Name = item[1].ToString()!;
            genres.Add(genre);
        }
        foreach (Genre item in genres)
        {
            System.Console.WriteLine(item.Id + " | " + item.Name);
        }
        return genres;
    }

    public void UpdateGenre(Genre genre)
    {
        using var connection = Database.Connection();
        var query = "select * from genres";
        NpgsqlDataAdapter adapter = new(query, connection);
        DataSet ds = new();
        adapter.Fill(ds);
        NpgsqlCommandBuilder builder = new(adapter);
        DataTable genresTable = ds.Tables[0];
        bool f = false;
        foreach (DataRow item in genresTable.Rows)
        {
            if (Convert.ToInt32(item["id"]) == genre.Id)
            {
                item["name"] = genre.Name;
                f = true;
                break;
            }
        }
        if (f)
        {
            adapter.Update(ds);
            System.Console.WriteLine("Updated!");
        }
        else System.Console.WriteLine("Genre not found");
    }
}
