using System.Data;
using Npgsql;
namespace Infrastruture.Data;

public class AppDbContext
{
    private readonly string? connectionString = "Host=localhost;Port=5432;Database=booknest;Username=postgres;Password=p000";
    public IDbConnection Connection() => new NpgsqlConnection(connectionString);

    public bool TestConnection(out string error)
    {
        try
        {
            using var connection = Connection();
            connection.Open();
            error = "";
            return true;
        }
        catch (InvalidOperationException ex)
        {
            error = ex.Message;
            return false;
        }
        catch (ArgumentException ex)
        {
            error = $"Неверная строка подключения: {ex.Message}";
            return false;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.InvalidPassword)
        {
            error = "Неверное имя пользователя или пароль.";
            return false;
        }
        catch (NpgsqlException ex)
        {
            error = ex.Message;
            return false;
        }
    }
}
