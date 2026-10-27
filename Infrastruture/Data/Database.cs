using Npgsql;

namespace Infrastruture.Data;

public static class Database
{
    private static readonly string? connectionString =
    Environment.GetEnvironmentVariable("booknest");
    public static NpgsqlConnection Connection()
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Переменная окружения 'booknest' не задана.");
        return new NpgsqlConnection(connectionString);
    }

    public static bool TestConnection(out string error)
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
